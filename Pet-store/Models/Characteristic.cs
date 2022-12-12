using System;
using System.Collections.Generic;

namespace Pet_store.Models
{
    public partial class Characteristic
    {
        public Characteristic()
        {
            ProductCharacteristics = new HashSet<ProductCharacteristic>();
        }

        public int Id { get; set; }
        public int IdFood { get; set; }
        public int IdGame { get; set; }
        public int IdAnimal { get; set; }

        public virtual Animal IdAnimalNavigation { get; set; } = null!;
        public virtual Food IdFoodNavigation { get; set; } = null!;
        public virtual Game IdGameNavigation { get; set; } = null!;
        public virtual ICollection<ProductCharacteristic> ProductCharacteristics { get; set; }
    }
}
