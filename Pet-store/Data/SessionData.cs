using Pet_store.Models;
using System.Windows;

namespace Pet_store.Data
{
    public static class SessionData
    {
        #region CurrentUser
        private static User _currentUser = new()
        {
            IdRole = Role.ROLE_GUEST,
            Lastname = "Гость",
            Name = "",
            Email = "",
            Password = "",
            Phone = "",
            DateOfBirthday = System.DateOnly.MinValue,
        };
        public static User CurrentUser
        {
            get => _currentUser;
            set => _currentUser = value;
        }
        #endregion

        public static UsersList? SelectedUser { get; set; }

        public static ProductIsInBasket? SelectedProduct { get; set; }

        #region CurrentWindow
        private static Window? _currentWindow = App.Current.Windows[0];
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
