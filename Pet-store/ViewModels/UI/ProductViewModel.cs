﻿using Pet_store.Data;
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
        #region Name
        private string? _name = SessionData.SelectedProduct.Product.Name;
        public string? Name
        {
            get => _name;
            set => Set(ref _name, value);
        }
        #endregion

        #region Rating
        private float? _rating = SessionData.SelectedProduct.Product.Rating;
        public float? Rating
        {
            get => _rating;
            set => Set(ref _rating, value);
        }
        #endregion

        #region Price
        private decimal _price = SessionData.SelectedProduct.Product.Price;
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
        private List<Category> _selectedCategories = SessionData.SelectedProduct.ProductCategories;
        public List<Category> SelectedCategories
        {
            get => _selectedCategories;
            set => Set(ref _selectedCategories, value);
        }
        #endregion

        #region SelectedCategoryCB
        private Category? _selectedCategoryCB;
        public Category? SelectedCategoryCB
        {
            get => _selectedCategoryCB;
            set => Set(ref _selectedCategoryCB, value);
        }
        #endregion

        #region SelectedCategoryDG
        private Category? _selectedCategoryDG;
        public Category? SelectedCategoryDG
        {
            get => _selectedCategoryDG;
            set => Set(ref _selectedCategoryDG, value);
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
            if (!_selectedCategories.Contains(_selectedCategoryCB!))
            {
                _selectedCategories.Add(_selectedCategoryCB);
                _selectedCategoryCB = null;
            }
        }
        #endregion

        #region DeleteCategory
        public ICommand DeleteCategory { get; }
        private bool _canDeleteCategoryCommandExcute(object p) => true;
        private void _onDeleteCategoryCommandExcuted(object p)
        {
            if (_selectedCategoryDG != null)
                _selectedCategories.Remove(_selectedCategoryDG);
        }
        #endregion

        #region AddProduct
        public ICommand AddProduct { get; }
        private bool _canAddProductCommandExcute(object p) => true;
        private void _onAddProductCommandExcuted(object p)
        {
            if (_name != null && _rating != null && _rating > 0 && _rating <= 5 && _selectedCategories.Count != 0)
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
