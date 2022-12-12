using Pet_store.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using World_of_books.Infrastructures.Commands;
using World_of_books.ViewModels.Base;

namespace Pet_store.ViewModels.UI
{
    internal class ProductViewModel : ViewModelBase
    {
        #region Fields
        #region ListUsers
        private string _name;
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }
        #endregion

        #region Rating
        private float? _rating;
        public float? Rating
        {
            get => _rating;
            set => Set(ref _rating, value);
        }
        #endregion

        #region Price
        private decimal _price;
        public decimal Price
        {
            get => _price;
            set => Set(ref _price, value);
        }
        #endregion

        #region CategoriesList
        private List<Category> _categoriesList = DataBaseContext.Instance.Categories.ToList();
        public List<Category> CategoriesList
        {
            get => _categoriesList;
            set => Set(ref _categoriesList, value);
        }
        #endregion

        #region SelectedCategories
        private List<Category> _selectedCategories;
        public List<Category> SelectedCategories
        {
            get => _selectedCategories;
            set => Set(ref _selectedCategories, value);
        }
        #endregion

        #region SelectedCategory
        private Category? _selectedCategory;
        public Category? SelectedCategory
        {
            get => _selectedCategory;
            set => Set(ref _selectedCategory, value);
        }
        #endregion
        #endregion

        public ProductViewModel()
        {
            AddCategory = new LambdaCommand(_onAddCategoryCommandExcuted, _canAddCategoryCommandExcute);
            AddProduct = new LambdaCommand(_onAddProductCommandExcuted, _canAddProductCommandExcute);
            DeleteCategory = new LambdaCommand(_onDeleteCategoryCommandExcuted, _canDeleteCategoryCommandExcute);
        }

        #region Commands
        #region AddCategory
        public ICommand AddCategory { get; }
        private bool _canAddCategoryCommandExcute(object p) => true;
        private void _onAddCategoryCommandExcuted(object p)
        {
            if (!_selectedCategories.Contains(_selectedCategory!))
            {
                _selectedCategories.Add(_selectedCategory);
                _selectedCategory = null;
            }
        }
        #endregion

        #region DeleteCategory
        public ICommand DeleteCategory { get; }
        private bool _canDeleteCategoryCommandExcute(object p) => true;
        private void _onDeleteCategoryCommandExcuted(object p)
        {
            if (_selectedCategory != null)
                _selectedCategories.Remove(_selectedCategory);
        }
        #endregion

        #region AddProduct
        public ICommand AddProduct { get; }
        private bool _canAddProductCommandExcute(object p) => true;
        private void _onAddProductCommandExcuted(object p)
        {
            if (_name != null && _rating != null)
            {
                Product newProduct = new Product()
                {
                    Name = _name,
                    Rating = _rating,
                    Price = _price
                };
                DataBaseContext.Instance.SaveChanges();

                if (_selectedCategories.Count != 0)
                    _selectedCategories.ForEach(c => new ProductCategory()
                    {
                        IdProduct = newProduct.Id,
                        IdCategory = c.Id
                    });
                DataBaseContext.Instance.SaveChanges();
            }
        }
        #endregion
        #endregion
    }
}
