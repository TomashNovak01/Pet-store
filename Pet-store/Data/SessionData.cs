using Pet_store.Models;
using System.Windows;

namespace Pet_store.Data
{
    public static class SessionData
    {
        #region CurrentUser
        private static User? _currentUser;
        public static User? CurrentUser
        {
            get => _currentUser;
            set
            {
                if (value == null)
                    _currentUser = new User()
                    {
                        IdRole = Role.ROLE_GUEST,
                        Name = "Гость"
                    };
                else
                    _currentUser = value;
            }
        }
        #endregion

        public static UsersList? SelectedUser { get; set; }

        public static ProductIsInBasket? SelectedProduct { get; set; }

        #region CurrentWindow
        private static Window? _currentWindow;
        public static Window? CurrentWindow
        {
            get => _currentWindow;
            set
            {
                _currentWindow?.Close();
                _currentWindow = value;
            }
        }
        #endregion

        public static Window? CurrentDialogue { get; set; }
    }
}
