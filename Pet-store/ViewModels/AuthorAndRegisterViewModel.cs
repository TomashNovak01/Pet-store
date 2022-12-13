using Pet_store.Data;
using Pet_store.Models;
using Pet_store.Views;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using World_of_books.Infrastructures.Commands;
using World_of_books.ViewModels.Base;
using World_of_books.ViewModels.GeneralViewModels;

namespace Pet_store.ViewModels
{
    internal class AuthorAndRegisterViewModel : ViewModelBase
    {
        #region Fields
        #region LastName
        [Required(ErrorMessage = "Не указана фамилия")]
        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set => Set(ref _lastName, value);
        }
        #endregion

        #region Name
        [Required(ErrorMessage = "Не указано имя")]
        private string _name;
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }
        #endregion

        #region Email
        private string _email;
        [Required(ErrorMessage = "Не указан электронный адрес почты")]
        [EmailAddress(ErrorMessage = "Некорректный адрес")]
        public string Email
        {
            get => _email;
            set => Set(ref _email, value);
        }
        #endregion

        #region Password
        private string _password;
        [Required(ErrorMessage = "Не указан пароль")]
        public string Password
        {
            get => _password;
            set => Set(ref _password, value);
        }
        #endregion

        #region DateOfBirthday
        private DateOnly _dateOfBirthday;
        [Required(ErrorMessage = "Не указана дата рождения")]
        public DateOnly DateOfBirthday
        {
            get => _dateOfBirthday;
            set => Set(ref _dateOfBirthday, value);
        }
        #endregion

        #region Phone
        private string _phone;
        public string Phone
        {
            get => _phone;
            set => Set(ref _phone, value);
        }
        #endregion
        #endregion

        public AuthorAndRegisterViewModel()
        {
            LogInCommand = new LambdaCommand(_onLogInCommandExcuted, _canLogInCommandExcute);
            CreateNewAccountCommand = new LambdaCommand(_onCreateNewAccountCommandExcuted, _canCreateNewAccountCommandExcute);
            GoToCatalog = new LambdaCommand(_onGoToCatalogCommandExcuted, _canGoToCatalogCommandExcute);
        }

        #region Commands
        #region LogInCommand
        public ICommand LogInCommand { get; }
        private bool _canLogInCommandExcute(object p) => true;
        private void _onLogInCommandExcuted(object p)
        {
            if (DataBaseContext.Instance.Users.FirstOrDefault(u => u.Email == Email && u.Password == Password) is User user)
            {
                SessionData.CurrentUser = user;

                if (user.IdRole == Role.ROLE_CUSTOMER)
                    SessionData.CurrentWindow = new Catalog();
                else if (user.IdRole == Role.ROLE_EMPLOYEE)
                    SessionData.CurrentWindow = new Employee();
                else if (user.IdRole == Role.ROLE_ADMINISTRATOR)
                    SessionData.CurrentWindow = new Administrator();

                SessionData.CurrentWindow.Show();
                SessionData.CurrentDialogue.Close();
            }
            else
                MessageBox.Show("Пользователь не найден!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        #endregion

        #region CreateNewAccountCommand
        public ICommand CreateNewAccountCommand { get; }
        private bool _canCreateNewAccountCommandExcute(object p) => true;
        private void _onCreateNewAccountCommandExcuted(object p)
        {
            if (DataCheck.TryLastAndFirstNameAndPassword(LastName, Name, Password) &&
                 DataCheck.TryEmail(Email) && DataCheck.TryNumberPhone(Phone))
            {
                SaveData();
                SessionData.CurrentWindow = new Catalog();
                SessionData.CurrentWindow.Show();
            }
            else
                DataCheck.ShowErrors();
        }

        private void SaveData()
        {
            SessionData.CurrentUser = new User()
            {
                IdRole = Role.ROLE_CUSTOMER,
                Lastname = LastName,
                Name = Name,
                Password = Password,
                Email = Email,
                Phone = Phone,
                DateOfBirthday = DateOfBirthday
            };

            DataBaseContext.Instance.Users.Add(SessionData.CurrentUser);
            DataBaseContext.Instance.SaveChanges();
        }
        #endregion

        #region GoToCatalog
        public ICommand GoToCatalog { get; }
        private bool _canGoToCatalogCommandExcute(object p) => true;
        private void _onGoToCatalogCommandExcuted(object p)
        {
            SessionData.CurrentWindow = new Catalog();
            SessionData.CurrentWindow.Show();
        }
        #endregion
        #endregion
    }
}
