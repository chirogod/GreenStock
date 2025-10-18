using GreenStock.Commands;
using GreenStock.DataBase;
using GreenStock.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GreenStock.ViewModels
{
    public class ClientViewModel : BaseViewModel
    {
        private readonly GenericRepository<ClientModel> _ClientRepository;
        private ObservableCollection<ClientModel> _Clients;
        private ClientModel _SelectedClient;
        private ClientModel _Client;

        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand OpenAddModalCommand { get; }
        public ICommand OpenUpdateModalCommand { get; }
        public ICommand CloseModalCommand { get; }

        private bool _IsAddModalOpen;
        private bool _IsUpdateModalOpen;
        public ClientViewModel()
        {
            _ClientRepository = new GenericRepository<ClientModel>();
            _Clients = new ObservableCollection<ClientModel>();
            _Client = new ClientModel();

            AddCommand = new AsyncCommand(AddClientExecuteAsync, AddClientCanExecute);
            UpdateCommand = new AsyncCommand(UpdateClientExecuteAsync, UpdateClientCanExecute);
            DeleteCommand = new AsyncCommand(DeleteClientExecuteAsync, DeleteClientCanExecute);

            OpenAddModalCommand = new RelayCommand(OpenAddModalExecute, OpenAddModalCanExecute);
            OpenUpdateModalCommand = new RelayCommand(OpenUpdateModalExecute, OpenUpdateModalCanExecute);
            CloseModalCommand = new RelayCommand(CloseModalExecute, CloseModalCanExecute);

            Task.Run(async () => await LoadClients());
        }

        public ObservableCollection<ClientModel> Clients
        {
            get => _Clients;
            set
            {
                if (value != _Clients)
                {
                    _Clients = value;
                    OnPropertyChanged(nameof(Clients));
                }
            }
        }

        public ClientModel Client
        {
            get => _Client;
            set
            {
                if (value != _Client)
                {
                    _Client = value;
                    OnPropertyChanged(nameof(Client));
                }
            }
        }

        public ClientModel SelectedClient
        {
            get => _SelectedClient;
            set
            {
                if(value != _SelectedClient)
                {
                    _SelectedClient = value;
                    OnPropertyChanged(nameof(SelectedClient));

                    if(_SelectedClient != null)
                    {
                        Client = new ClientModel
                        {
                            Id = _SelectedClient.Id,
                            FullName = _SelectedClient.FullName,
                            Phone = _SelectedClient.Phone
                        };
                    }
                }
            }
        }

        public bool IsAddModalOpen
        {
            get => _IsAddModalOpen;
            set
            {
                _IsAddModalOpen = value;
                OnPropertyChanged(nameof(IsAddModalOpen));
            }
        }
        public bool IsUpdateModalOpen
        {
            get => _IsUpdateModalOpen;
            set
            {
                _IsUpdateModalOpen = value;
                OnPropertyChanged(nameof(IsUpdateModalOpen));
            }
        }
        private bool AddClientCanExecute(object x)
        {
            return true;
        }
        private async Task AddClientExecuteAsync(object x)
        {
            await _ClientRepository.Add(Client);
            await LoadClients();
            Client = new ClientModel();
        }

        private bool UpdateClientCanExecute(object x)
        {
            return true;
        }
        private async Task UpdateClientExecuteAsync(object x)
        {
            await _ClientRepository.Update(Client);
            await LoadClients();
            Client = new ClientModel();
        }

        private bool DeleteClientCanExecute(object x)
        {
            return true;
        }
        private async Task DeleteClientExecuteAsync(object x)
        {
            await _ClientRepository.Delete(Client);
            await LoadClients();
            Client = new ClientModel();
        }

        private bool OpenAddModalCanExecute(object x)
        {
            return true;
        }
        private void OpenAddModalExecute(object x)
        {
            IsAddModalOpen = true;
        }

        private bool OpenUpdateModalCanExecute(object x)
        {
            return true;
        }
        private void OpenUpdateModalExecute(object x)
        {
            IsUpdateModalOpen = true;
        }

        private bool CloseModalCanExecute(object x)
        {
            return true;
        }
        private void CloseModalExecute(object x)
        {
            IsAddModalOpen = false;
            IsUpdateModalOpen = false;
        }

        private async Task LoadClients()
        {
            _Clients = await _ClientRepository.GetAllAsync();
            Clients = new ObservableCollection<ClientModel>(_Clients);
        }




    }
}
