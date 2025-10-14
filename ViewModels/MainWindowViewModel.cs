using GreenStock.Commands;
using GreenStock.DataBase.Interfaces;
using GreenStock.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GreenStock.ViewModels
{
    class MainWindowViewModel : BaseViewModel
    {
        private BaseViewModel _currentViewModel;
        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                if (_currentViewModel != value)
                {
                    _currentViewModel = value;
                    OnPropertyChanged(nameof(CurrentViewModel));
                }
            }
        }
        public ICommand NavigateToDashboardCommand { get; }
        public ICommand NavigateToSupplierCommand { get; }
        public ICommand NavigateToCatalogoCommand { get; }
        public ICommand NavigateToProductCommand { get; }
        public ICommand NavigateToClientCommand { get; }
        public ICommand NavigateToUserCommand { get; }
        public ICommand NavigateToConfigCommand { get; }
        public MainWindowViewModel()
        {

            // Usar _serviceProvider.GetService<T>() en los comandos:
            NavigateToDashboardCommand = new RelayCommand(o => CurrentViewModel = new DashboardViewModel());
            NavigateToSupplierCommand = new RelayCommand(o => CurrentViewModel = new SupplierViewModel());
            NavigateToCatalogoCommand = new RelayCommand(o => CurrentViewModel = new CatalogoViewModel());
            NavigateToProductCommand = new RelayCommand(o => CurrentViewModel = new ProductViewModel());
            NavigateToClientCommand = new RelayCommand(o => CurrentViewModel = new ClientViewModel());
            NavigateToUserCommand = new RelayCommand(o => CurrentViewModel = new UserViewModel());
            NavigateToConfigCommand = new RelayCommand(o => CurrentViewModel = new ConfigViewModel());

            // Establecer el VM inicial (también debe obtenerse del contenedor)
            CurrentViewModel = new DashboardViewModel();
        }
    }
}
