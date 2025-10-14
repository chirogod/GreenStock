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
    public class CatalogoViewModel : BaseViewModel
    {
        private readonly GenericRepository<CategoryModel> _CategoryRepository;
        private readonly GenericRepository<BrandModel> _BrandRepository;

        private ObservableCollection<CategoryModel> _Categories;
        private ObservableCollection<BrandModel> _Brands;

        private BrandModel _Brand;
        private CategoryModel _Category;

        private BrandModel _SelectedBrand;
        private CategoryModel _SelectedCategory;

        public ICommand AddBrand { get; }
        public ICommand UpdateBrand { get; }
        public ICommand DeleteBrand { get; }
        public ICommand AddCategory { get; }
        public ICommand UpdateCategory { get; }
        public ICommand DeleteCategory { get; }

        public ICommand OpenCategoryView { get; }
        public ICommand OpenBrandView { get; }

        public ICommand OpenAddCategoryModal { get; }
        public ICommand OpenUpdateCategoryModal { get; }
        public ICommand OpenAddBrandModal { get; }
        public ICommand OpenUpdateBrandModal { get; }
        public ICommand CloseModalCommand { get; }

        private bool _IsCategoryViewVisible;
        private bool _IsBrandViewVisible;

        private bool _AddCategoryModalOpen;
        private bool _UpdateCategoryModalOpen;
        private bool _AddBrandModalOpen;
        private bool _UpdateBrandModalOpen;
        public CatalogoViewModel()
        {
            _CategoryRepository = new GenericRepository<CategoryModel>();
            _BrandRepository = new GenericRepository<BrandModel>();
            _Categories = new ObservableCollection<CategoryModel>();
            _Brands = new ObservableCollection<BrandModel>();
            _Brand = new BrandModel();
            _Category = new CategoryModel();

            AddBrand = new AsyncCommand(AddBrandExecute, AddBrandCanExecute);
            UpdateBrand = new AsyncCommand(UpdateBrandExecute, UpdateBrandCanExecute);
            DeleteBrand = new AsyncCommand(DeleteBrandExecute, DeleteBrandCanExecute);
            AddCategory = new AsyncCommand(AddCategoryExecute, AddCategoryCanExecute);
            UpdateCategory = new AsyncCommand(UpdateCategoryExecute, UpdateCategoryCanExecute);
            DeleteCategory = new AsyncCommand(DeleteCategoryExecute, DeleteCategoryCanExecute);

            OpenCategoryView = new RelayCommand(OpenCategoryViewExecute, OpenCategoryViewCanExecute);
            OpenBrandView = new RelayCommand(OpenBrandViewExecute, OpenBrandViewCanExecute);

            _IsCategoryViewVisible = true;
            _IsBrandViewVisible = false;

            OpenAddCategoryModal = new RelayCommand(OpenAddCategoryModalExecute, OpenAddCategoryModalCanExecute);
            OpenUpdateCategoryModal = new RelayCommand(OpenUpdateCategoryModalExecute, OpenUpdateCategoryModalCanExecute );

            OpenAddBrandModal = new RelayCommand(OpenAddBrandModalExecute, OpenAddBrandModalCanExecute);
            OpenUpdateBrandModal = new RelayCommand(OpenUpdateBrandModalExecute, OpenUpdateBrandModalCanExecute);

            CloseModalCommand = new RelayCommand(CloseModalCommandExecute, CloseModalCommandCanExecute);

            Task.Run(async () => 
            {
                await LoadBrands();
                await LoadCategories();
            });
        }
        public BrandModel Brand
        {
            get => _Brand;
            set
            {
                if (value != _Brand)
                {
                    _Brand = value;
                    OnPropertyChanged(nameof(Brand));
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

        public BrandModel SelectedBrand
        {
            get => _SelectedBrand;
            set
            {
                if (value != _SelectedBrand)
                {
                    _SelectedBrand = value;
                    OnPropertyChanged(nameof(SelectedBrand));

                    if (_SelectedBrand != null)
                    {
                        Brand = new BrandModel
                        {
                             Id = _SelectedBrand.Id,
                            Name = _SelectedBrand.Name,
                            Description = _SelectedBrand.Description
                        };
                    }
                }
            }
        }

        public bool AddBrandCanExecute(object x)
        {
            return true;
        }
        private async Task AddBrandExecute(object x)
        {
            await _BrandRepository.Add(Brand);
            await LoadBrands();
            Brand = new BrandModel();
        }

        public bool UpdateBrandCanExecute(object x)
        {
            return true;
        }
        private async Task UpdateBrandExecute(object x)
        {
            await _BrandRepository.Update(Brand);
            await LoadBrands();
            Brand = new BrandModel();
        }

        public bool DeleteBrandCanExecute(object x)
        {
            return true;
        }
        private async Task DeleteBrandExecute(object x)
        {
            await _BrandRepository.Delete(Brand);
            await LoadBrands();
            Brand = new BrandModel();
        }

        public async Task LoadBrands()
        {
            var brands = await _BrandRepository.GetAllAsync();
            Brands = new ObservableCollection<BrandModel>(brands);
        }
        public CategoryModel Category
        {
            get => _Category;
            set
            {
                if (value != _Category)
                {
                    _Category = value;
                    OnPropertyChanged(nameof(Category));
                }
            }
        }
        public CategoryModel SelectedCategory
        {
            get => _SelectedCategory;
            set
            {
                if (value != _SelectedCategory)
                {
                    _SelectedCategory = value;
                    OnPropertyChanged(nameof(SelectedCategory));
                    if (_SelectedCategory != null)
                    {
                        Category = new CategoryModel
                        {
                            Id = _SelectedCategory.Id,
                            Name = _SelectedCategory.Name,
                            Description = _SelectedCategory.Description
                        };
                    }
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
        private bool AddCategoryCanExecute(object x)
        {
            return true;
        }
        private async Task AddCategoryExecute(object x)
        {
            await _CategoryRepository.Add(Category);
            await LoadCategories();
            Category = new CategoryModel();

        }
        private bool UpdateCategoryCanExecute(object x)
        {
            return true;
        }
        private async Task UpdateCategoryExecute(object x)
        {
            await _CategoryRepository.Update(Category);
            await LoadCategories();
            Category = new CategoryModel();
        }
        private bool DeleteCategoryCanExecute(object x)
        {
            return true;
        }
        private async Task DeleteCategoryExecute(object x)
        {
            if (x is CategoryModel categoryToDelete)
            {
                await _CategoryRepository.Delete(categoryToDelete);

                await LoadCategories();

                Category = new CategoryModel();
                SelectedCategory = null;
            }
        }

        public async Task LoadCategories()
        {
            var categories = await _CategoryRepository.GetAllAsync();
            Categories = new ObservableCollection<CategoryModel>(categories);
        }


        public bool AddCategoryModalOpen
        {
            get => _AddCategoryModalOpen;
            set
            {
                if (value != _AddCategoryModalOpen)
                {
                    _AddCategoryModalOpen = value;
                    OnPropertyChanged(nameof(AddCategoryModalOpen));
                }
            }
        }

        public bool UpdateCategoryModalOpen
        {
            get => _UpdateCategoryModalOpen;
            set
            {
                if (value != _UpdateCategoryModalOpen)
                {
                    _UpdateCategoryModalOpen = value;
                    OnPropertyChanged(nameof(UpdateCategoryModalOpen));
                }
            }
        }
        public bool AddBrandModalOpen
        {
            get => _AddBrandModalOpen;
            set
            {
                if (value != _AddBrandModalOpen)
                {
                    _AddBrandModalOpen = value;
                    OnPropertyChanged(nameof(AddBrandModalOpen));
                }
            }
        }

        public bool UpdateBrandModalOpen
        {
            get => _UpdateBrandModalOpen;
            set
            {
                if (value != _UpdateBrandModalOpen)
                {
                    _UpdateBrandModalOpen = value;
                    OnPropertyChanged(nameof(UpdateBrandModalOpen));
                }
            }
        }

        private bool OpenAddCategoryModalCanExecute(object x)
        {
            return true;
        }
        public void OpenAddCategoryModalExecute(object x)
        {
            AddCategoryModalOpen = true;
            Category = new CategoryModel();
        }
        private bool OpenUpdateCategoryModalCanExecute(object x)
        {
            return true;
        }
        public void OpenUpdateCategoryModalExecute(object x)
        {
            if (x is CategoryModel categoryToSelect)
            {
                SelectedCategory = categoryToSelect;

                UpdateCategoryModalOpen = true;
            }
        }
        private bool OpenAddBrandModalCanExecute(object x)
        {
            return true;
        }
        public void OpenAddBrandModalExecute(object x)
        {
            AddBrandModalOpen = true;
            Brand = new BrandModel();
        }
        private bool OpenUpdateBrandModalCanExecute(object x)
        {
            return true;
        }
        public void OpenUpdateBrandModalExecute(object x)
        {
            UpdateBrandModalOpen = true;
        }
        private bool CloseModalCommandCanExecute(object x)
        {
            return true;
        }
        public void CloseModalCommandExecute(object x)
        {
            AddCategoryModalOpen = false;
            UpdateCategoryModalOpen = false;
            AddBrandModalOpen = false;
            UpdateBrandModalOpen = false;
        }

        public bool IsCategoryViewVisible
        {
            get => _IsCategoryViewVisible;
            set
            {
                if (value != _IsCategoryViewVisible)
                {
                    _IsCategoryViewVisible = value;
                    OnPropertyChanged(nameof(IsCategoryViewVisible));
                }
            }
        }
        public bool IsBrandViewVisible
        {
            get => _IsBrandViewVisible;
            set
            {
                if (value != _IsBrandViewVisible)
                {
                    _IsBrandViewVisible = value;
                    OnPropertyChanged(nameof(IsBrandViewVisible));
                }
            }
        }

        private bool OpenCategoryViewCanExecute(object x)
        {
            return true;
        }
        public void OpenCategoryViewExecute(object x)
        {
            IsCategoryViewVisible = true;
            IsBrandViewVisible = false;
        }
        private bool OpenBrandViewCanExecute(object x)
        {
            return true;
        }
        public void OpenBrandViewExecute(object x)
        {
            IsCategoryViewVisible = false;
            IsBrandViewVisible = true;
        }
    }
}
