using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenStock.Models
{
    public class ClientModel
    {
        private int _Id;
        private string _FullName;
        private string _Phone;
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
            get=> _FullName;
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
    }
}