using GreenStock.Commands;
using GreenStock.DataBase;
using GreenStock.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GreenStock.ViewModels
{
    public class SaleViewModel : BaseViewModel
    {
        private readonly GenericRepository<SaleModel> _SaleRepository;
        private readonly GenericRepository<ProductModel> _ProductRepository;
        private ObservableCollection<ProductModel> _Products;
        private ObservableCollection<SaleItemModel> _SaleItems;
        private SaleItemModel _SaleItem;
        private SaleModel _Sale;
        private ProductModel _Product;
        private ProductModel _SelectedProduct;

        public ICommand AddToSaleItems { get; }
        public ICommand AddWeighedSaleItem { get; }
        public ICommand CloseModalCommand { get; }

        public ICommand UpdateSelectedSaleItemCommand { get; }
        public ICommand DeleteSelectedSaleItemCommand { get; }

        public ICommand ApplySaleDiscountCommand { get; }

        private bool _IsBalanzaModalOpen;
        private decimal _BalanzaWeight;

        private string FilterProduct;

        public SaleViewModel()
        {
            _ProductRepository = new GenericRepository<ProductModel>();
            _Products = new ObservableCollection<ProductModel>();
            _SaleItems = new ObservableCollection<SaleItemModel>();

            _Sale = new SaleModel();

            AddToSaleItems = new RelayCommand(AddToSaleItemsExecute, AddToSaleItemsCanExecute);
            AddWeighedSaleItem = new RelayCommand(AddWeighedSaleItemExecute, AddWeighedSaleItemCanExecute);

            UpdateSelectedSaleItemCommand = new RelayCommand(UpdateSelectedSaleItemExecute, UpdateSelectedSaleItemCanExecute);
            DeleteSelectedSaleItemCommand = new RelayCommand(DeleteSelectedSaleItemExecute, DeleteSelectedSaleItemCanExecute);

            ApplySaleDiscountCommand = new RelayCommand(ApplySaleDiscountExecute, ApplySaleDiscountCanExecute);

            CloseModalCommand = new RelayCommand(CloseModalExecute, CloseModalCanExecute);


            Task.Run(async () => await LoadProductsAsync(""));
        }
        public SaleModel Sale
        {
            get => _Sale;
            set
            {
                if (_Sale != value)
                {
                    _Sale = value;
                    OnPropertyChanged(nameof(Sale));
                }
            }
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
        public SaleItemModel SaleItem
        {
            get => _SaleItem;
            set
            {
                if(_SaleItem != value)
                {
                    _SaleItem = value;
                    OnPropertyChanged(nameof(SaleItem));
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

        public string ProductFilter
        {
            get => FilterProduct;
            set
            {
                if (FilterProduct != value)
                {
                    FilterProduct = value;
                    OnPropertyChanged(nameof(ProductFilter));
                    LoadProductsAsync(ProductFilter);
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
                if (_SaleItems != value)
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
                Discount = 0
            };
            _SaleItems.Add(SaleItem);
            UpdateSaleTotal();
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

        private bool UpdateSelectedSaleItemCanExecute(object x)
        {
            return true;
        }
        private void UpdateSelectedSaleItemExecute(object x)
        {
            UpdateSaleTotal();
        }

        private bool DeleteSelectedSaleItemCanExecute(object x)
        {
            return true;
        }
        private void DeleteSelectedSaleItemExecute(object x)
        {
            if (SaleItem != null)
            {
                _SaleItems.Remove(SaleItem);
            }

            UpdateSaleTotal();

        }

        private bool ApplySaleDiscountCanExecute(object x)
        {
            return true;
        }
        private void ApplySaleDiscountExecute(object x)
        {
            UpdateSaleTotal();
        }

        private void UpdateSaleTotal()
        {
            _Sale.SubTotal = _SaleItems.Sum(item => item.Total);

            OnPropertyChanged(nameof(Sale));
            OnPropertyChanged(nameof(SaleItems));
            OnPropertyChanged(nameof(SaleItem));
        }

        private async Task LoadProductsAsync(string filter)
        {
            Expression<Func<ProductModel, bool>> productPredicate = null;

            if (!string.IsNullOrWhiteSpace(filter))
            {
                productPredicate = p =>
                    p.Name.Contains(filter) ||
                    p.Code.Contains(filter) ||
                    p.Plu.Contains(filter);
            }
            var products = await _ProductRepository.GetAllAsync(productPredicate);
            Products = new ObservableCollection<ProductModel>(products);
        }

    }
}
