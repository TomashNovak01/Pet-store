using Pet_store.Data;
using Pet_store.Models;
using Pet_store.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using World_of_books.Infrastructures.Commands;
using World_of_books.ViewModels.Base;

namespace Pet_store.ViewModels
{
    internal class EmployeeViewModel : ViewModelBase
    {
        #region Fields
        public static User CurrentUser => SessionData.CurrentUser;

        #region ListUsers
        private static List<User> _listUsers = DataBaseContext.Instance.Users.Where(u => u.IdRole == Role.ROLE_CUSTOMER).ToList();
        public List<User> ListUsers
        {
            get => _listUsers;
            set => Set(ref _listUsers, value);
        }
        #endregion

        #region SearchListUser
        private List<string> _searchListUser;
        public List<string> SearchListUser
        {
            get => _searchListUser;
            set => Set(ref _searchListUser, value);
        }
        #endregion

        #region SearchUser
        private string _searchUser;
        public string SearchUser
        {
            get => _searchUser;
            set => Set(ref _searchUser, value);
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

        public EmployeeViewModel()
        {
            InitSearchListUser();

            GoToAuthorization = new LambdaCommand(_onGoToAuthorizationCommandExcuted, _canGoToAuthorizationCommandExcute);
            GetReadyStatus = new LambdaCommand(_onGetReadyStatusCommandExcuted, _canGetReadyStatusCommandExcute);
            UpdateListUserCommand = new LambdaCommand(_onUpdateListUserCommandExcuted, _canUpdateListUserCommandExcute);
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

        #region GetReadyStatus
        public ICommand GetReadyStatus { get; }
        private bool _canGetReadyStatusCommandExcute(object p) => true;
        private void _onGetReadyStatusCommandExcuted(object p) => DataBaseContext.Instance.SaveChanges();
        #endregion

        #region InitSearchListUser
        private void InitSearchListUser()
        {
            _searchListUser.Clear();
            _searchListUser.Add("Отмена поиска");

            foreach (var user in DataBaseContext.Instance.Users)
            {
                _searchListUser.Add(user.Lastname);
                _searchListUser.Add(user.Name);
            }
        }
        #endregion

        #region UpdateListUserCommand
        public ICommand UpdateListUserCommand { get; }
        private bool _canUpdateListUserCommandExcute(object p) => !string.IsNullOrEmpty(_searchUser);
        private void _onUpdateListUserCommandExcuted(object p)
        {
            ListUsers = DataBaseContext.Instance.Users.Where(u => u.IdRole == Role.ROLE_CUSTOMER).ToList();

            if (_searchUser != "Отмена поиска")
                ListUsers = ListUsers.Where(u => u.FullName.ToLower().Contains(_searchUser.ToLower())).ToList();
        }
        #endregion
        #endregion
    }
}
