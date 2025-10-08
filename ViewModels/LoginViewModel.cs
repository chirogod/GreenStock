using GreenStock.Commands;
using GreenStock.DataBase;
using GreenStock.DataBase.Interfaces;
using GreenStock.Models;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GreenStock.ViewModels
{
    internal class LoginViewModel : BaseViewModel
    {
        private readonly LoginRepository LoginRepository;
        private UserModel _User;
        public ICommand LoginCommand { get; }

        private string _ErrorMessage;
        private bool _Visibility;

        public LoginViewModel()
        {
            LoginRepository = new LoginRepository();
            _User = new UserModel();
            LoginCommand = new AsyncCommand(LoginExecuteAsync, LoginCanExecute);
            Visibility = true;
        }
        public UserModel User
        {
            get => _User;
            set
            {
                if (value != _User)
                {
                    _User = value;
                    OnPropertyChanged(nameof(User));
                }
            }
        }
        public string ErrorMessage
        {
            get => _ErrorMessage;
            set
            {
                if (value != _ErrorMessage)
                {
                    _ErrorMessage = value;
                    OnPropertyChanged(nameof(ErrorMessage));
                }
            }
        }
        public bool Visibility
        {
            get => _Visibility;
            set
            {
                if (value != _Visibility)
                {
                    _Visibility = value;
                    OnPropertyChanged(nameof(Visibility));
                }
            }
        }

        private bool LoginCanExecute(object x)
        {
            return (User.Username != "" && User.Password != "");
        }
        private async Task LoginExecuteAsync(object x)
        {
            ErrorMessage = string.Empty;
            var user = await LoginRepository.validateUser(User.Username, User.Password);
            if (user != null)
            {
                ErrorMessage = "Login successful!";
                Visibility = false;
            }
            else
            {
                ErrorMessage = "Datos incorrectos";
                return;
            }
        }
    }
}
