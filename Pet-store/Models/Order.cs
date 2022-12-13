using System;
using System.Collections.Generic;

namespace Pet_store.Models
{
    public partial class Order
    {
        public Order()
        {
            Baskets = new HashSet<Basket>();
        }

        public int Id { get; set; }
        public int IdUser { get; set; }
        public DateTime DateOfOrder { get; set; }
        public decimal Price { get; set; }

        public bool IsReady { get; set; }

        public virtual User IdUserNavigation { get; set; } = null!;
        public virtual ICollection<Basket> Baskets { get; set; }
    }
}
