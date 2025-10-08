using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenStock.Models
{
    public class ProductModel
    {
        private int _Id;
        private string _Code;
        private string _Name;
        private string _Description;
        private int _SupplierId;
        private int _BrandId;
        private int _CategoryId;
        private bool _Pesable;
        private string _Plu;
        private decimal _StockActual;
        private decimal _StockMinimo;
        private decimal _CostoSinIva;
        private decimal _CostoConIva;
        private decimal _VentaSinIva;
        private decimal _VentaConIva;

        public virtual SupplierModel Supplier { get; set; }
        public virtual BrandModel Brand { get; set; }
        public virtual CategoryModel Category { get; set; }

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
        public string Code
        {
            get => _Code;
            set
            {
                if (value != _Code)
                {
                    _Code = value;
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
        public int SupplierId
        {
            get => _SupplierId;
            set
            {
                if (value != _SupplierId)
                {
                    _SupplierId = value;
                }
            }
        }
        public int BrandId
        {
            get=> _BrandId;
            set
            {
                if (value != _BrandId)
                {
                    _BrandId = value;
                }
            }
        }
        public int CategoryId
        {
            get => _CategoryId;
            set
            {
                if (value != _CategoryId)
                {
                    _CategoryId = value;
                }
            }
        }
        public bool Pesable
        {
            get => _Pesable;
            set
            {
                if (value != _Pesable)
                {
                    _Pesable = value;
                }
            }
        }
        public string Plu
        {
            get => _Plu;
            set
            {
                if (value != _Plu)
                {
                    _Plu = value;
                }
            }
        }
        public decimal StockActual
        {
            get => _StockActual;
            set
            {
                if (value != _StockActual)
                {
                    _StockActual = value;
                }
            }
        }
        public decimal StockMinimo
        {
            get => _StockMinimo;
            set
            {
                if (value != _StockMinimo)
                {
                    _StockMinimo = value;
                }
            }
        }
        public decimal CostoSinIva
        {
            get => _CostoSinIva;
            set
            {
                if (value != _CostoSinIva)
                {
                    _CostoSinIva = value;
                }
            }
        }
        public decimal CostoConIva
        {
            get => _CostoConIva;
            set
            {
                if (value != _CostoConIva)
                {
                    _CostoConIva = value;
                }
            }
        }
        public decimal VentaSinIva
        {
            get => _VentaSinIva;
            set
            {
                if (value != _VentaSinIva)
                {
                    _VentaSinIva = value;
                }
            }
        }
        public decimal VentaConIva
        {
            get => _VentaConIva; 
            set
            {
                if (value != _VentaConIva)
                {
                    _VentaConIva = value;
                }
            }
        }
    }
}
