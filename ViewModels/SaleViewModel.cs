using GreenStock.Commands;
using GreenStock.DataBase;
using GreenStock.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GreenStock.ViewModels
{
    public class SaleViewModel : BaseViewModel
    {
        private readonly GenericRepository<ProductModel> _ProductRepository;
        private ObservableCollection<ProductModel> _Products;
        private ObservableCollection<SaleItemModel> _SaleItems;
        private SaleItemModel _SaleItem;
        private ProductModel _Product;
        private ProductModel _SelectedProduct;

        public ICommand AddToSaleItems { get; }
        public ICommand AddWeighedSaleItem { get; }
        public ICommand CloseModalCommand { get; }

        private bool _IsBalanzaModalOpen;
        private decimal _BalanzaWeight;

        public SaleViewModel()
        {
            _ProductRepository = new GenericRepository<ProductModel>();
            _Products = new ObservableCollection<ProductModel>();
            _SaleItems = new ObservableCollection<SaleItemModel>();

            AddToSaleItems = new RelayCommand(AddToSaleItemsExecute, AddToSaleItemsCanExecute);
            AddWeighedSaleItem = new RelayCommand(AddWeighedSaleItemExecute, AddWeighedSaleItemCanExecute);
            CloseModalCommand = new RelayCommand(CloseModalExecute, CloseModalCanExecute);


            Task.Run(async () => await LoadProductsAsync());
        }

        public ProductModel Product
        {
            get => _Product;
            set
            {
                if(_Product != value)
                {
                    _Product = value;
                    OnPropertyChanged(nameof(Product));
                }
            }
        }
        public ProductModel SelectedProduct
        {
            get => _SelectedProduct;
            set
            {
                if(_SelectedProduct != value)
                {
                    _SelectedProduct = value;
                    OnPropertyChanged(nameof(SelectedProduct));
                    if(_SelectedProduct != null)
                    {
                        Product = new ProductModel
                        {
                            Id = _SelectedProduct.Id,
                            Code = _SelectedProduct.Code,
                            Name = _SelectedProduct.Name,
                            Description = _SelectedProduct.Description,
                            SupplierId = _SelectedProduct.SupplierId,
                            BrandId = _SelectedProduct.BrandId,
                            CategoryId = _SelectedProduct.CategoryId,
                            Medida = _SelectedProduct.Medida,
                            Pesable = _SelectedProduct.Pesable,
                            Plu = _SelectedProduct.Plu,
                            StockActual = _SelectedProduct.StockActual,
                            StockMinimo = _SelectedProduct.StockMinimo,
                            CostoSinIva = _SelectedProduct.CostoSinIva,
                            CostoConIva = _SelectedProduct.CostoConIva,
                            VentaSinIva = _SelectedProduct.VentaSinIva,
                            VentaConIva = _SelectedProduct.VentaConIva
                        };
                    }
                }
            }
        }

        public bool IsBalanzaModalOpen
        {
            get => _IsBalanzaModalOpen;
            set
            {
                if(_IsBalanzaModalOpen != value)
                {
                    _IsBalanzaModalOpen = value;
                    OnPropertyChanged(nameof(IsBalanzaModalOpen));
                }
            }
        }
        public decimal BalanzaWeight
        {
            get => _BalanzaWeight;
            set
            {
                if(_BalanzaWeight != value)
                {
                    _BalanzaWeight = value;
                    OnPropertyChanged(nameof(BalanzaWeight));
                }
            }
        }

        public bool CloseModalCanExecute(object x)
        {
            return true;
        }
        public void CloseModalExecute(object x)
        {
            IsBalanzaModalOpen = false;
        }

        public ObservableCollection<ProductModel> Products
        {
            get => _Products;
            set
            {
                if(_Products != value)
                {
                    _Products = value;
                    OnPropertyChanged(nameof(Products));
                }
            }
        }
        public ObservableCollection<SaleItemModel> SaleItems
        {
            get => _SaleItems;
            set
            {
                if(_SaleItems != value)
                {
                    _SaleItems = value;
                    OnPropertyChanged(nameof(SaleItems));
                }
            }
        }
        private bool AddToSaleItemsCanExecute(object parameter)
        {
            return true;
        }
        private void AddToSaleItemsExecute(object parameter)
        {
            if(parameter is ProductModel productToAdd)
            {
                SelectedProduct = productToAdd;
                if (SelectedProduct != null)
                {
                    if (SelectedProduct.Pesable)
                    {
                        IsBalanzaModalOpen = true;
                        return;

                    }
                    CreateAddSaleItem(SelectedProduct, 1);
                }
            }
            
        }

        private void CreateAddSaleItem(ProductModel product, decimal quantity)
        {
            var SaleItem = new SaleItemModel
            {
                ProductId = product.Id,
                Product = product,
                Quantity = quantity,
                SalePrice = product.VentaConIva,
                Discount = 0,
                Total = (product.VentaConIva * quantity)
            };
            _SaleItems.Add(SaleItem);
        }

        private bool AddWeighedSaleItemCanExecute(object x)
        {
            return true;
        }
        private void AddWeighedSaleItemExecute(object x)
        {
            if(SelectedProduct !=null && BalanzaWeight > 0)
            {
                CreateAddSaleItem(SelectedProduct, BalanzaWeight);
                
            }
            BalanzaWeight = 0;
            IsBalanzaModalOpen = false;
        }
        private async Task LoadProductsAsync()
        {
            var products = await _ProductRepository.GetAllAsync();
            Products = new ObservableCollection<ProductModel>(products);
        }

    }
}
