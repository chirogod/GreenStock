using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenStock.Models
{
    public class CashRegisterModel
    {
        private int _Id;
        private string _Name;
        private DateTime _OpenDate;
        private decimal _InitialAmount;
        private DateTime _CloseDate;
        private decimal _FinalAmount;
        private decimal _Diference;
        private string _Status;

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
        public DateTime OpenDate
        {
            get=> _OpenDate;
            set
            {
                if (value != _OpenDate)
                {
                    _OpenDate = value;
                }
            }
        }
        public decimal InitialAmount
        {
            get => _InitialAmount;
            set
            {
                if (value != _InitialAmount)
                {
                    _InitialAmount = value;
                }
            }
        }
        public DateTime CloseDate
        {
            get => _CloseDate;
            set
            {
                if (value != _CloseDate)
                {
                    _CloseDate = value;
                }
            }
        }
        public decimal FinalAmount
        {
            get => _FinalAmount;
            set
            {
                if (value != _FinalAmount)
                {
                    _FinalAmount = value;
                }
            }
        }
        public decimal Diference
        {
            get => _Diference;
            set
            {
                if (value != _Diference)
                {
                    _Diference = value;
                }
            }
        }
        public string Status
        {
            get => _Status;
            set
            {
                if (value != _Status)
                {
                    _Status = value;
                }
            }
        }
    }
}
