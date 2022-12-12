using Pet_store.Views.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World_of_books.ViewModels.Base;

namespace Pet_store.ViewModels.UI
{
    internal class ProductUserControlViewModel : ViewModelBase
    {
        #region Fields
        #region Name
        private string? _name;
        public string? Name
        {
            get => _name;
            set => Set(ref _name, value);
        }
        #endregion

        #region Rating
        private float? _rating;
        public float? Rating
        {
            get => _rating;
            set => Set(ref _rating, value);
        }
        #endregion

        #region Price
        private decimal? _price;
        public decimal? Price
        {
            get => _price;
            set => Set(ref _price, value);
        }
        #endregion

        #region Category
        private string? _category;
        public string? Category
        {
            get => _category;
            set => Set(ref _category, value);
        }
        #endregion
        #endregion

        public ProductUserControlViewModel()
        {

        }

        #region Commands
        #endregion
    }
}
