using GreenStock.Commands;
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
        public ICommand NavigateToConfigCommand { get; }
        public MainWindowViewModel()
        {
            NavigateToDashboardCommand = new RelayCommand(o => CurrentViewModel = new DashboardViewModel());
            NavigateToConfigCommand = new RelayCommand(o => CurrentViewModel = new ConfigViewModel());
            CurrentViewModel = new DashboardViewModel();
        }
    }
}
