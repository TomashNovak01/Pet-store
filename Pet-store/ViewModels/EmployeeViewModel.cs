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
        private List<User> _listUsers = DataBaseContext.Instance.Users.Where(u => u.IdRole == Role.ROLE_CUSTOMER).ToList();
        public List<User> ListUsers
        {
            get => _listUsers;
            set => Set(ref _listUsers, value);
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
            GoToAuthorization = new LambdaCommand(_onGoToAuthorizationCommandExcuted, _canGoToAuthorizationCommandExcute);
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
        #endregion
    }
}
