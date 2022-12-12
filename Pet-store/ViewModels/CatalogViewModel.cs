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
        private List<string> _searchList;
        public List<string> SearchList
        {
            get => _searchList;
            set => Set(ref _searchList, value);
        }
        #endregion

        #region CurrentUser
        public static User User => SessionData.CurrentUser;
        #endregion
        #endregion

        public CatalogViewModel()
        {
            //FillSearchList();

            GoToAuthorization = new LambdaCommand(_onGoToAuthorizationCommandExcuted, _canGoToAuthorizationCommandExcute);
            CreateAnOrder = new LambdaCommand(_onCreateAnOrderCommandExcuted, _canCreateAnOrderCommandExcute);
            AddInBasket = new LambdaCommand(_onAddInBasketCommandExcuted, _canAddInBasketCommandExcute);
            InitCurrentWindow = new LambdaCommand(_onInitCurrentWindowCommandExcuted, _canInitCurrentWindowCommandExcute);
        }

        #region Commands
        #region GoToAuthorization
        public ICommand GoToAuthorization { get; }
        private bool _canGoToAuthorizationCommandExcute(object p) => true;
        private void _onGoToAuthorizationCommandExcuted(object p)
        {
            SessionData.CurrentDialogue = new AuthorAndRegister();
            SessionData.CurrentDialogue.Show();
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

        #region AddInBasket
        public ICommand AddInBasket { get; }
        private bool _canAddInBasketCommandExcute(object p) => true;
        private void _onAddInBasketCommandExcuted(object p) => ProductsInBasket = DataBaseContext.Instance.InBasket.Where(p => p.IsInBasket).ToList();
        #endregion

        #region CreateAnOrder
        public ICommand CreateAnOrder { get; }
        private bool _canCreateAnOrderCommandExcute(object p) => true;
        private void _onCreateAnOrderCommandExcuted(object p)
        {
            if (SessionData.CurrentUser.IdRole != Role.ROLE_CUSTOMER || SessionData.CurrentUser == null)
            {
                SessionData.CurrentWindow = new AuthorAndRegister();
                SessionData.CurrentWindow.Show();
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
