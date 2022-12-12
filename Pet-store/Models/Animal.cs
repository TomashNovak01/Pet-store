using System;
using System.Collections.Generic;

namespace Pet_store.Models
{
    public partial class Animal
    {
        public Animal()
        {
            Characteristics = new HashSet<Characteristic>();
        }

        public int Id { get; set; }
        public string Race { get; set; } = null!;

        public virtual ICollection<Characteristic> Characteristics { get; set; }
    }
}
