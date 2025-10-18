using GreenStock.DataBase;
using GreenStock.Models;
using GreenStock.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GreenStock.DataBase.Interfaces;
using System.Security.RightsManagement;

namespace GreenStock.ViewModels
{
    public class SupplierViewModel : BaseViewModel
    {
        private readonly GenericRepository<SupplierModel> _supplierRepository;
        private ObservableCollection<SupplierModel> _Suppliers;
        private SupplierModel _Supplier;
        private SupplierModel _SelectedSupplier;

        public ICommand AddSupplierCommand { get; }
        public ICommand UpdateSupplierCommand { get; }
        public ICommand DeleteSupplierCommand { get; }

        public ICommand OpenModalCommand { get; }
        public ICommand OpenUpdateModalCommand { get; }
        public ICommand CloseModalCommand { get; }

        private bool _IsUpdateModalOpen;
        private bool _IsModalOpen;

        public SupplierViewModel()
        {
            _supplierRepository = new GenericRepository<SupplierModel>();
            _Suppliers = new ObservableCollection<SupplierModel>();
            _Supplier = new SupplierModel();

            AddSupplierCommand = new AsyncCommand(AddSupplierExecuteAsync, AddSupplierCanExecute);
            UpdateSupplierCommand = new AsyncCommand(UpdateSupplierExecuteAsync, UpdateSupplierCanExecute);
            DeleteSupplierCommand = new AsyncCommand(DeleteSupplierExecuteAsync, DeleteSupplierCanExecute);

            OpenModalCommand = new RelayCommand(OpenModalExecute, OpenModalCanExecute);
            OpenUpdateModalCommand = new RelayCommand(OpenUpdateModalExecute, OpenModalCanExecute);
            CloseModalCommand = new RelayCommand(CloseModalExecute, CloseModalCanExecute);

            Task.Run(async () => await LoadSuppliers());
        }

        public SupplierModel Supplier
        {
            get => _Supplier; 
            set 
            { 
                if (value != _Supplier) 
                { 
                    _Supplier = value; 
                    OnPropertyChanged(nameof(Supplier)); 
                }
            }
        }
        public SupplierModel SelectedSupplier
        {
            get => _SelectedSupplier;
            set
            {
                if (_SelectedSupplier != value)
                {
                    _SelectedSupplier = value;
                    OnPropertyChanged(nameof(SelectedSupplier));

                    if (_SelectedSupplier != null)
                    {
                        Supplier = new SupplierModel
                        {
                            Id = _SelectedSupplier.Id,
                            Name = _SelectedSupplier.Name,
                            Description = _SelectedSupplier.Description,
                            Cuit = _SelectedSupplier.Cuit,
                            Phone = _SelectedSupplier.Phone,
                            Email = _SelectedSupplier.Email,
                            Notes = _SelectedSupplier.Notes
                        };
                    }
                }
            }
        }
        public ObservableCollection<SupplierModel> Suppliers
        {
            get => _Suppliers;
            set
            {
                if (_Suppliers != value)
                {
                    _Suppliers = value;
                    OnPropertyChanged(nameof(Suppliers));
                }
            }
        }

        /// <summary>
        ///  FUNCIONES DE LA LOGICA DE LOS MODALES
        /// </summary>
        public bool IsModalOpen
        {
            get => _IsModalOpen;
            set
            {
                _IsModalOpen = value;
                OnPropertyChanged(nameof(IsModalOpen));
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
        private void OpenUpdateModalExecute(object obj)
        {
            IsUpdateModalOpen = true;
        }
        private void OpenModalExecute(object obj)
        {
            Supplier = new SupplierModel();
            IsModalOpen = true;
        }
        private bool OpenModalCanExecute(object obj)
        {
            return true;
        }
        private bool CloseModalCanExecute(object obj)
        {
            return true;
        }

        private void CloseModalExecute(object obj)
        {
            IsModalOpen = false;
            IsUpdateModalOpen = false;
        }

        /// funciones para aniadir editar o eliminar
        private bool AddSupplierCanExecute(object obj)
        {
            return true;
        }

        private async Task AddSupplierExecuteAsync(object arg)
        {
            await _supplierRepository.Add(Supplier);
            await LoadSuppliers();
            Supplier = new SupplierModel();
        }

        private bool UpdateSupplierCanExecute(object obj)
        {
            return true;
        }

        private async Task UpdateSupplierExecuteAsync(object arg)
        {
            await _supplierRepository.Update(Supplier);
            await LoadSuppliers();
            Supplier = new SupplierModel();
        }

        private bool DeleteSupplierCanExecute(object obj)
        {
            return true;
        }

        private async Task DeleteSupplierExecuteAsync(object arg)
        {
            await _supplierRepository.Delete(Supplier);
            await LoadSuppliers();
            Supplier = new SupplierModel();
        }

        private async Task LoadSuppliers()
        {
            _Suppliers = await _supplierRepository.GetAllAsync();
            OnPropertyChanged(nameof(Suppliers));
        }
    }
}
