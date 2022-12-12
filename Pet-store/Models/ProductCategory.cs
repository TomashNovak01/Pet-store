using System;
using System.Collections.Generic;

namespace Pet_store.Models
{
    public partial class ProductCategory
    {
        public int Id { get; set; }
        public int IdProduct { get; set; }
        public int IdCategory { get; set; }

        public virtual Category IdCategoryNavigation { get; set; } = null!;
        public virtual Product IdProductNavigation { get; set; } = null!;
    }
}
