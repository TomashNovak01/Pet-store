using System;
using System.Collections.Generic;

namespace Pet_store.Models
{
    public partial class Basket
    {
        public int Id { get; set; }
        public int IdOrder { get; set; }
        public int IdProduct { get; set; }
        public int Count { get; set; }

        public virtual Order IdOrderNavigation { get; set; } = null!;
        public virtual Product IdProductNavigation { get; set; } = null!;
    }
}
