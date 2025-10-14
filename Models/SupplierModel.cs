using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenStock.Models
{
    public class SupplierModel
    {
        private int _Id;
        private string _Name;
        private string _Description;
        private string _Cuit;
        private string _Phone;
        private string _Email;
        private string _Notes;

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
        public string Name
        {
            get => _Name;
            set
            {
                if (value != _Name)
                {
                    _Name = value;
                }
            }
        }
        public string Description
        {
            get => _Description;
            set
            {
                if (value != _Description)
                {
                    _Description = value;
                }
            }
        }
        public string Cuit
        {
            get => _Cuit;
            set
            {
                if (value != _Cuit)
                {
                    _Cuit = value;
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
        public string Email
        {
            get => _Email;
            set
            {
                if (value != _Email)
                {
                    _Email = value;
                }
            }
        }
        public string Notes
        {
            get => _Notes;
            set
            {
                if (value != _Notes)
                {
                    _Notes = value;
                }
            }
        }
    }
}
