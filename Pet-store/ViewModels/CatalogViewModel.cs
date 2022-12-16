using Pet_store.Data;
using Pet_store.Models;
using Pet_store.Views;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using World_of_books.Infrastructures.Commands;
using World_of_books.ViewModels.Base;

namespace Pet_store.ViewModels
{
    internal class CatalogViewModel : ViewModelBase
    {
        #region Fields
        public static User User => SessionData.CurrentUser;

        #region ListProduct
        private List<ProductIsInBasket> _listProduct = DataBaseContext.Instance.InBasket;
        public List<ProductIsInBasket> ListProduct
        {
            get => _listProduct;
            set => Set(ref _listProduct, value);
        }
        #endregion

        #region ProductsInBasket
        private List<ProductIsInBasket> _productsInBasket;
        public List<ProductIsInBasket> ProductsInBasket
        {
            get => _productsInBasket;
            set => Set(ref _productsInBasket, value);
        }
        #endregion

        #region SearchList
        private List<string> _searchList = DataBaseContext.Instance.Products.Select(c => c.Name).ToList();
        public List<string> SearchList
        {
            get => _searchList;
            set => Set(ref _searchList, value);
        }
        #endregion

        #region Search
        private string _search;
        public string Search
        {
            get => _search;
            set => Set(ref _search, value);
        }
        #endregion

        #region IsVisibleMyOrders
        private object _isVisibleMyOrders = Visibility.Collapsed;
        public object IsVisibleMyOrders
        {
            get => _isVisibleMyOrders;
            set
            {
                if (User.IdRole == Role.ROLE_CUSTOMER)
                    _isVisibleMyOrders = Visibility.Visible;
                else
                    _isVisibleMyOrders = Visibility.Collapsed;
            }
        }
        #endregion        
        #endregion

        public CatalogViewModel()
        {
            FillSearchList();

            GoToAuthorization = new LambdaCommand(_onGoToAuthorizationCommandExcuted, _canGoToAuthorizationCommandExcute);
            UpdateProductListCommand = new LambdaCommand(_onUpdateProductListCommandExcuted, _canUpdateProductListCommandExcute);
            CreateAnOrder = new LambdaCommand(_onCreateAnOrderCommandExcuted, _canCreateAnOrderCommandExcute);
            EditBasketCommand = new LambdaCommand(_onEditBasketCommandExcuted, _canEditBasketCommandExcute);
            InitCurrentWindow = new LambdaCommand(_onInitCurrentWindowCommandExcuted, _canInitCurrentWindowCommandExcute);
        }

        #region Commands
        #region GoToAuthorization
        public ICommand GoToAuthorization { get; }
        private bool _canGoToAuthorizationCommandExcute(object p) => true;
        private void _onGoToAuthorizationCommandExcuted(object p)
        {
            SessionData.CurrentDialogue = new AuthorAndRegister();
            SessionData.CurrentDialogue.ShowDialog();
        }
        #endregion

        #region FillSearchList
        private void FillSearchList()
        {
            _searchList.Clear();
            _searchList.Add("Отмена поиска");

            foreach (var product in DataBaseContext.Instance.Products)
                _searchList.Add(product.Name);

            foreach (var category in DataBaseContext.Instance.Categories)
                _searchList.Add(category.Name);
        }
        #endregion

        #region UpdateProductListCommand
        public ICommand UpdateProductListCommand { get; }
        private bool _canUpdateProductListCommandExcute(object p) => !string.IsNullOrEmpty(_search);
        private void _onUpdateProductListCommandExcuted(object p)
        {
            ListProduct = DataBaseContext.Instance.InBasket;

            if(_search != "Отмена поиска")
                ListProduct = _listProduct.Where(p => p.Product.Name.ToLower().Contains(_search.ToLower()) ||
                                                      p.ProductCategories.Select(c => c.Name.ToLower()).Contains(_search.ToLower())).ToList();
        }
        #endregion

        #region EditBasketCommand
        public ICommand EditBasketCommand { get; }
        private bool _canEditBasketCommandExcute(object p) => true;
        private void _onEditBasketCommandExcuted(object p) => ProductsInBasket = DataBaseContext.Instance.InBasket.Where(p => p.IsInBasket).ToList();
        #endregion

        #region CreateAnOrder
        public ICommand CreateAnOrder { get; }
        private bool _canCreateAnOrderCommandExcute(object p) => true;
        private void _onCreateAnOrderCommandExcuted(object p)
        {
            if (SessionData.CurrentUser.IdRole != Role.ROLE_CUSTOMER || SessionData.CurrentUser == null)
            {
                SessionData.CurrentWindow = new AuthorAndRegister();
                SessionData.CurrentWindow.ShowDialog();
                return;
            }

            if (_productsInBasket.Count() == 0)
            {
                MessageBox.Show("Добавьте товары в корзину", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Order newOrder = new Order()
            {
                IdUser = SessionData.CurrentUser.Id,
                DateOfOrder = System.DateTime.Now,
                Price = ProductsInBasket.Sum(p => p.Product.Price)
            };
            DataBaseContext.Instance.Orders.Add(newOrder);
            DataBaseContext.Instance.SaveChanges();

            foreach (var productIsInBasket in _productsInBasket)
                DataBaseContext.Instance.Baskets.Add(new Basket()
                {
                    IdOrder = newOrder.Id,
                    IdProduct = productIsInBasket.Product.Id,
                    Count = productIsInBasket.Count
                });

            DataBaseContext.Instance.SaveChanges();
        }
        #endregion

        #region InitCurrentWindow
        public ICommand InitCurrentWindow { get; }
        private bool _canInitCurrentWindowCommandExcute(object p) => true;
        private void _onInitCurrentWindowCommandExcuted(object p) => SessionData.CurrentWindow = new Catalog();
        #endregion
        #endregion
    }
}
