using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenStock.Models
{
    public class SaleModel
    {
        private int _Id;
        private DateTime _Date;
        private decimal _SubTotal;
        private decimal _Discount;
        private decimal _Total;
        private int _ClientId;
        private string _Observations;
        public virtual ClientModel Client { get; set; }
        public virtual ICollection<SaleItemModel> SaleItems { get; set; }

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
        public DateTime Date
        {
            get=> _Date;
            set
            {
                if (value != _Date)
                {
                    _Date = value;
                }
            }
        }
        public decimal SubTotal
        {
            get => _SubTotal;
            set
            {
                if (value != _SubTotal)
                {
                    _SubTotal = value;
                }
            }
        }
        public decimal Discount
        {
            get => _Discount;
            set
            {
                if (value != _Discount)
                {
                    _Discount = value;
                }
            }
        }
        public decimal Total
        {
            get => _Total;
            set
            {
                if (value != _Total)
                {
                    _Total = value;
                }
            }
        }
        public int ClientId
        {
            get => _ClientId;
            set
            {
                if (value != _ClientId)
                {
                    _ClientId = value;
                }
            }
        }
        public string Observations
        {
            get => _Observations;
            set
            {
                if (value != _Observations)
                {
                    _Observations = value;
                }
            }
        }
    }

    public class SaleItemModel
    {
        private int _Id;
        private int _SaleId;
        private int _ProductId;
        private decimal _SalePrice;
        private decimal _Discount;
        private decimal _Quantity;
        private decimal _Total;
        public virtual ProductModel Product { get; set; }
        public virtual SaleModel Sale { get; set; }

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
        public int SaleId
        {
            get => _SaleId;
            set
            {
                if (value != _SaleId)
                {
                    _SaleId = value;
                }
            }
        }
        public int ProductId
        {
            get => _ProductId;
            set
            {
                if (value != _ProductId)
                {
                    _ProductId = value;
                }
            }
        }
        public decimal SalePrice
        {
            get => _SalePrice;
            set
            {
                if (value != _SalePrice)
                {
                    _SalePrice = value;
                }
            }
        }
        public decimal Discount
        {
            get => _Discount;
            set
            {
                if (value != _Discount)
                {
                    _Discount = value;
                }
            }
        }
        public decimal Quantity
        {
            get => _Quantity;
            set
            {
                if (value != _Quantity)
                {
                    _Quantity = value;
                }
            }
        }
        public decimal Total
        {
            get => _Total;
            set
            {
                if (value != _Total)
                {
                    _Total = value;
                }
            }
        }
    }
}
