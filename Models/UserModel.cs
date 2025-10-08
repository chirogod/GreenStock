using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenStock.Models
{
    public class UserModel
    {
        private int _Id;
        private string _FullName;
        private string _Phone;
        private string _Username;
        private string _Password;
        private string _Role;

        public int Id
        {
            get => _Id;
            set
            {
                if (value != _Id)
                {
                    _Id = value;
                }
            }
        }
        public string FullName
        {
            get => _FullName;
            set
            {
                if (value != _FullName)
                {
                    _FullName = value;
                }
            }
        }
        public string Phone
        {
            get => _Phone;
            set
            {
                if (value != _Phone)
                {
                    _Phone = value;
                }
            }
        }
        public string Username
        {
            get => _Username;
            set
            {
                if (value != _Username)
                {
                    _Username = value;
                }
            }
        }
        public string Password
        {
            get => _Password;
            set
            {
                if (value != _Password)
                {
                    _Password = value;
                }
            }
        }
        public string Role
        {
            get => _Role;
            set
            {
                if (value != _Role)
                {
                    _Role = value;
                }
            }
        }
    }
}
