using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenStock.Models
{
    internal class StoreModel
    {
        private int _Id;
        private string _Name;
        private string _License;
        private string _Token;

        public int Id
        {
            get => _Id;
            set{
                if(_Id != value)
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
                if (_Name != value)
                {
                    _Name = value;
                }
            }
        }

        public string License
        {
            get => _License;
            set
            {
                if (_License != value)
                {
                    _License = value;
                }
            }
        }

        public string Token
        {
            get => _Token;
            set
            {
                if (_Token != value)
                {
                    _Token = value;
                }
            }
        }
    }
}
