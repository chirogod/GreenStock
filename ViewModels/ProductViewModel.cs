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
    public class ProductViewModel : BaseViewModel
    {
        private readonly GenericRepository<ProductModel> _ProductRepository;
        private readonly GenericRepository<SupplierModel> _SupplierRepository;
        private readonly GenericRepository<CategoryModel> _CategoryRepository;
        private readonly GenericRepository<BrandModel> _BrandRepository;
        private ObservableCollection<ProductModel> _Products;
        private ObservableCollection<CategoryModel> _Categories;
        private ObservableCollection<BrandModel> _Brands;
        private ObservableCollection<SupplierModel> _Suppliers;
        private ProductModel _Product;
        private ProductModel _SelectedProduct;

        public ICommand AddProductCommand { get; }
        public ICommand UpdateProductCommand { get; }
        public ICommand DeleteProductCommand { get; }
        public ICommand OpenAddModalCommand { get; }
        public ICommand OpenUpdateModalCommand { get; }
        public ICommand CloseModalCommand { get; }

        public bool _IsAddModalOpen;
        public bool _IsUpdateModalOpen;

        public ProductViewModel()
        {
            _ProductRepository = new GenericRepository<ProductModel>();
            _SupplierRepository = new GenericRepository<SupplierModel>();
            _CategoryRepository = new GenericRepository<CategoryModel>();
            _BrandRepository = new GenericRepository<BrandModel>();

            _Products = new ObservableCollection<ProductModel>();
            _Categories = new ObservableCollection<CategoryModel>();
            _Brands = new ObservableCollection<BrandModel>();
            _Suppliers = new ObservableCollection<SupplierModel>();
            _Product = new ProductModel();

            AddProductCommand = new AsyncCommand(AddProductExecuteAsync, AddProductCanExecute);
            UpdateProductCommand = new AsyncCommand(UpdateProductExecuteAsync, UpdateProductCanExecute);
            DeleteProductCommand = new AsyncCommand(DeleteProductExecuteAsync, DeleteProductCanExecute);

            OpenAddModalCommand = new RelayCommand(OpenAddModalExecute, OpenAddModalCanExecute);
            OpenUpdateModalCommand = new RelayCommand(OpenUpdateModalExecute, OpenUpdateModalCanExecute);
            CloseModalCommand = new RelayCommand(CloseModalExecute, CloseModalCanExecute);

            Task.Run(async () => {
                await LoadProducts();
                await LoadEntitiesRelated();
            } );
        }

        public ProductModel Product
        {
            get => _Product;
            set
            {
                if(value != _Product)
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
                if (value != _SelectedProduct)
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

        public ObservableCollection<ProductModel> Products
        {
            get => _Products;
            set
            {
                if (value != _Products)
                {
                    _Products = value;
                    OnPropertyChanged(nameof(Products));
                }
            }
        }
        public ObservableCollection<CategoryModel> Categories
        {
            get => _Categories;
            set
            {
                if (value != _Categories)
                {
                    _Categories = value;
                    OnPropertyChanged(nameof(Categories));
                }
            }
        }
        public ObservableCollection<BrandModel> Brands
        {
            get => _Brands;
            set
            {
                if (value != _Brands)
                {
                    _Brands = value;
                    OnPropertyChanged(nameof(Brands));
                }
            }
        }
        public ObservableCollection<SupplierModel> Suppliers
        {
            get => _Suppliers;
            set
            {
                if (value != _Suppliers)
                {
                    _Suppliers = value;
                    OnPropertyChanged(nameof(Suppliers));
                }
            }
        }

        public bool AddProductCanExecute(object x)
        {
            return true;
        }

        public async Task AddProductExecuteAsync(object x)
        {
            await _ProductRepository.Add(Product);
            await LoadProducts();
            Product = new ProductModel();
        }

        public bool UpdateProductCanExecute(object x)
        {
            return true;
        }

        public async Task UpdateProductExecuteAsync(object x)
        {
            await _ProductRepository.Update(Product);
            await LoadProducts();
            Product = new ProductModel();
        }

        public bool DeleteProductCanExecute(object x)
        {
            return true;
        }

        public async Task DeleteProductExecuteAsync(object x)
        {
            await _ProductRepository.Delete(Product);
            await LoadProducts();
        }

        public async Task LoadProducts()
        {
            _Products = await _ProductRepository.GetAllAsync(p=>p.Category, p=>p.Brand, p=>p.Supplier);
            OnPropertyChanged(nameof(Products));
        }
        public async Task LoadEntitiesRelated()
        {
            Categories = await _CategoryRepository.GetAllAsync();
            Suppliers = await _SupplierRepository.GetAllAsync();
            Brands = await _BrandRepository.GetAllAsync();
        }


        // MODAL COMMANDS
        public bool IsAddModalOpen
        {
            get => _IsAddModalOpen;
            set
            {
                if (value != _IsAddModalOpen)
                {
                    _IsAddModalOpen = value;
                    OnPropertyChanged(nameof(IsAddModalOpen));
                }
            }
        }
        public bool IsUpdateModalOpen
        {
            get => _IsUpdateModalOpen;
            set
            {
                if (value != _IsUpdateModalOpen)
                {
                    _IsUpdateModalOpen = value;
                    OnPropertyChanged(nameof(IsUpdateModalOpen));
                }
            }
        }
        private bool OpenAddModalCanExecute(object x)
        {
            return true;
        }
        private void OpenAddModalExecute(object x)
        {
            IsAddModalOpen = true;
        }
        private bool OpenUpdateModalCanExecute(object x)
        {
            return true;
        }   
        private void OpenUpdateModalExecute(object x)
        {
            IsUpdateModalOpen = true;
        }
        private bool CloseModalCanExecute(object x)
        {
            return true;
        }
        private void CloseModalExecute(object x)
        {
            IsAddModalOpen = false;
            IsUpdateModalOpen = false;
        }


    }
}
