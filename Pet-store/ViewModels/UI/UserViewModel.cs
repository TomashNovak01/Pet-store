using Pet_store.Data;
using Pet_store.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Input;
using World_of_books.Infrastructures.Commands;
using World_of_books.ViewModels.Base;
using World_of_books.ViewModels.GeneralViewModels;

namespace Pet_store.ViewModels.UI
{
    internal class UserViewModel : ViewModelBase
    {
        #region Fields
        #region ListRoles
        private List<Role> _listRoles = DataBaseContext.Instance.Roles.ToList();
        public List<Role> ListRoles
        {
            get => _listRoles;
            set => Set(ref _listRoles, value);
        }
        #endregion

        #region SelectedRole
        private Role _selectedRole = SessionData.SelectedUser.UserRole;
        public Role SelectedRole
        {
            get => _selectedRole;
            set => Set(ref _selectedRole, value);
        }
        #endregion

        #region LastName
        [Required(ErrorMessage = "Не указана фамилия")]
        private string _lastName = SessionData.SelectedUser.User.Lastname;
        public string LastName
        {
            get => _lastName;
            set => Set(ref _lastName, value);
        }
        #endregion

        #region Name
        [Required(ErrorMessage = "Не указано имя")]
        private string _name = SessionData.SelectedUser.User.Name;
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }
        #endregion

        #region Email
        private string _email = SessionData.SelectedUser.User.Email;
        [Required(ErrorMessage = "Не указан электронный адрес почты")]
        [EmailAddress(ErrorMessage = "Некорректный адрес")]
        public string Email
        {
            get => _email;
            set => Set(ref _email, value);
        }
        #endregion

        #region Password
        private string _password = SessionData.SelectedUser.User.Password;
        [Required(ErrorMessage = "Не указан пароль")]
        public string Password
        {
            get => _password;
            set => Set(ref _password, value);
        }
        #endregion

        #region DateOfBirthday
        private DateOnly? _dateOfBirthday = SessionData.SelectedUser.User.DateOfBirthday;
        [Required(ErrorMessage = "Не указана дата рождения")]
        public DateOnly? DateOfBirthday
        {
            get => _dateOfBirthday;
            set => Set(ref _dateOfBirthday, value);
        }
        #endregion

        #region Phone
        private string _phone = SessionData.SelectedUser.User.Phone;
        public string Phone
        {
            get => _phone;
            set => Set(ref _phone, value);
        }
        #endregion
        #endregion

        public UserViewModel()
        {
            CreateNewAccountCommand = new LambdaCommand(_onCreateNewAccountCommandExcuted, _canCreateNewAccountCommandExcute);
        }

        #region Commands
        #region CreateNewAccountCommand
        public ICommand CreateNewAccountCommand { get; }
        private bool _canCreateNewAccountCommandExcute(object p) => true;
        private void _onCreateNewAccountCommandExcuted(object p)
        {
            if (DataCheck.TryLastAndFirstNameAndPassword(LastName, Name, Password) &&
                 DataCheck.TryEmail(Email) && DataCheck.TryNumberPhone(Phone) && SelectedRole != null)
            {
                SaveData();
                SessionData.CurrentDialogue.Close();
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
        #endregion
    }
}
