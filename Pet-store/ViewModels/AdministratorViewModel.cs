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
    internal class AdministratorViewModel : ViewModelBase
    {
        #region Fields
        public static User CurrentUser => SessionData.CurrentUser;

        #region ListUsers
        public static List<UsersList> _listUsers = DataBaseContext.Instance.UsersLists;
        public List<UsersList> ListUsers
        {
            get => _listUsers;
            set => Set(ref _listUsers, value);
        }
        #endregion

        #region SelectedUser
        private UsersList _selectedUser;
        public UsersList SelectedUser
        {
            get => _selectedUser;
            set => Set(ref _selectedUser, value);
        }
        #endregion

        #region ListProduct
        public static List<ProductIsInBasket> _listProduct = DataBaseContext.Instance.InBasket;
        public List<ProductIsInBasket> ListProduct
        {
            get => _listProduct;
            set => Set(ref _listProduct, value);
        }
        #endregion

        #region SelectedProduct
        private ProductIsInBasket _selectedProduct;
        public ProductIsInBasket SelectedProduct
        {
            get => _selectedProduct;
            set => Set(ref _selectedProduct, value);
        }
        #endregion

        #region ListBaskets
        private List<Basket> _listBaskets = DataBaseContext.Instance.Baskets.ToList();
        public List<Basket> ListBaskets
        {
            get => _listBaskets;
            set => Set(ref _listBaskets, value);
        }
        #endregion

        #region ListOrders
        private List<Order> _listOrders = DataBaseContext.Instance.Orders.ToList();
        public List<Order> ListOrders
        {
            get => _listOrders;
            set => Set(ref _listOrders, value);
        }
        #endregion
        #endregion

        public AdministratorViewModel()
        {
            GoToAuthorization = new LambdaCommand(_onGoToAuthorizationCommandExcuted, _canGoToAuthorizationCommandExcute);
            GoToUser = new LambdaCommand(_onGoToUserCommandExcuted, _canGoToUserCommandExcute);
            EditUser = new LambdaCommand(_onEditUserCommandExcuted, _canEditUserCommandExcute);
            DeleteUser = new LambdaCommand(_onDeleteUserCommandExcuted, _canDeleteUserCommandExcute);
            GoToProduct = new LambdaCommand(_onGoToProductCommandExcuted, _canGoToProductCommandExcute);
            EditProduct = new LambdaCommand(_onEditProductCommandExcuted, _canEditProductCommandExcute);
            DeleteProduct = new LambdaCommand(_onDeleteProductCommandExcuted, _canDeleteProductCommandExcute);
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

        #region GoToUser
        public ICommand GoToUser { get; }
        private bool _canGoToUserCommandExcute(object p) => true;
        private void _onGoToUserCommandExcuted(object p)
        {
            SessionData.SelectedUser = null;
            SessionData.CurrentDialogue = new Views.UI.User();
            SessionData.CurrentDialogue.Show();

            _listUsers = DataBaseContext.Instance.UsersLists;
        }
        #endregion

        #region EditUser
        public ICommand EditUser { get; }
        private bool _canEditUserCommandExcute(object p) => true;
        private void _onEditUserCommandExcuted(object p)
        {
            if (_selectedUser != null && SelectedUser.User.IdRole != Role.ROLE_ADMINISTRATOR)
            {
                SessionData.SelectedUser = SelectedUser;
                SessionData.CurrentDialogue = new Views.UI.User();
                SessionData.CurrentDialogue.Show();
            }
            else
                MessageBox.Show("Вы не можете редактировать администраторов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        #endregion

        #region DeleteUser
        public ICommand DeleteUser { get; }
        private bool _canDeleteUserCommandExcute(object p) => true;
        private void _onDeleteUserCommandExcuted(object p)
        {
            if (_selectedUser != null && SelectedUser.UserRole.Id != Role.ROLE_ADMINISTRATOR)
            {
                DataBaseContext.Instance.Users.Remove(SelectedUser.User);
                DataBaseContext.Instance.SaveChanges();
            }
            else
                MessageBox.Show("Вы не можете удалять администраторов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        #endregion

        #region GoToProduct
        public ICommand GoToProduct { get; }
        private bool _canGoToProductCommandExcute(object p) => true;
        private void _onGoToProductCommandExcuted(object p)
        {
            SessionData.SelectedProduct = null;
            SessionData.CurrentDialogue = new Views.UI.Product();
            SessionData.CurrentDialogue.Show();

            ListProduct = DataBaseContext.Instance.InBasket;
        }
        #endregion

        #region EditProduct
        public ICommand EditProduct { get; }
        private bool _canEditProductCommandExcute(object p) => true;
        private void _onEditProductCommandExcuted(object p)
        {
            if (_selectedProduct != null)
            {
                SessionData.SelectedProduct = SelectedProduct;
                SessionData.CurrentDialogue = new Views.UI.Product();
                SessionData.CurrentDialogue.Show();
            }
        }
        #endregion

        #region DeleteProduct
        public ICommand DeleteProduct { get; }
        private bool _canDeleteProductCommandExcute(object p) => true;
        private void _onDeleteProductCommandExcuted(object p)
        {
            if (_selectedProduct != null)
            {
                DataBaseContext.Instance.Products.Remove(SelectedProduct.Product);
                DataBaseContext.Instance.SaveChanges();
            }
        }
        #endregion
        #endregion
    }
}
