using System;
using System.Collections.Generic;

namespace Pet_store.Models
{
    public partial class Category
    {
        public Category()
        {
            ProductCategories = new HashSet<ProductCategory>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<ProductCategory> ProductCategories { get; set; }

        public override string ToString() => Name;
    }
}
