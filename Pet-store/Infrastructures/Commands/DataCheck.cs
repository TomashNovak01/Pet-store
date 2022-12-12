using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace World_of_books.ViewModels.GeneralViewModels
{
    internal static class DataCheck
    {
        public static List<string> ListErrors = new();

        public static bool TryLastAndFirstNameAndPassword(params string[] parameters)
        {
            foreach (var parameter in parameters)
                if (string.IsNullOrEmpty(parameter))
                {
                    ListErrors.Add("фамилия или имя или пароль");
                    return false;
                }
            return true;
        }

        public static bool TryEmail(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
                var regex = new Regex(pattern, RegexOptions.IgnoreCase);

                if (!regex.IsMatch(email))
                {
                    ListErrors.Add("почта");
                    return false;
                }
                return true;
            }
            else
            {
                ListErrors.Add("почта");
                return false;
            }
        }

        public static bool TryNumberPhone(string phone)
        {
            if (phone.Length < 11 || !long.TryParse(phone, out _))
            {
                ListErrors.Add("номер телефона");
                return false;
            }
            return true;
        }

        public static void ShowErrors()
        {
            if (ListErrors.Count > 0)
            {
                MessageBox.Show(
                    $"Исправьте поля: {string.Join(", ", ListErrors.Select(e => e))}.",
                    "Внимание",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                ListErrors.Clear();
            }
        }
    }
}