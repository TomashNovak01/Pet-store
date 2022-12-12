using System;
using System.Collections.Generic;

namespace Pet_store.Models
{
    public partial class ProductCharacteristic
    {
        public int Id { get; set; }
        public int IdProduct { get; set; }
        public int IdCharacteristic { get; set; }

        public virtual Characteristic IdCharacteristicNavigation { get; set; } = null!;
        public virtual Product IdProductNavigation { get; set; } = null!;
    }
}
