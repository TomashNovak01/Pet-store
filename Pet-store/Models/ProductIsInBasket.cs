using System;
using System.Collections.Generic;
using System.Linq;

namespace Pet_store.Models
{
    public class ProductIsInBasket
    {
        public Product? Product { get; set; }
        public string CategoriesName
        {
            get
            {
                var categoryId = Product?.ProductCategories.Select(pc => pc.IdCategory);
                var categories = DataBaseContext.Instance.Categories.Where(c => categoryId.Contains(c.Id));

                return string.Join('\n', categories.Select(c => c.Name));
            }
        }

        public List<Category> ProductCategories
        {
            get
            {
                var categoryId = Product?.ProductCategories.Select(pc => pc.IdCategory);
                var categories = DataBaseContext.Instance.Categories.Where(c => categoryId.Contains(c.Id));

                return categories.ToList();
            }
        }
        public bool IsInBasket { get; set; }
        public int Count { get; set; } = 1;

        public decimal ProductTotalCost => Product.Price * Count;
    }
}
