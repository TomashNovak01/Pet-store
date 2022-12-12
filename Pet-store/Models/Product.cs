using System;
using System.Collections.Generic;

namespace Pet_store.Models
{
    public partial class Product
    {
        public Product()
        {
            Baskets = new HashSet<Basket>();
            ProductCategories = new HashSet<ProductCategory>();
            ProductCharacteristics = new HashSet<ProductCharacteristic>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public float? Rating { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<Basket> Baskets { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
        public virtual ICollection<ProductCharacteristic> ProductCharacteristics { get; set; }
    }
}
