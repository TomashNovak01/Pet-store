using System;
using System.Collections.Generic;

namespace Pet_store.Models
{
    public partial class Food
    {
        public Food()
        {
            Characteristics = new HashSet<Characteristic>();
        }

        public int Id { get; set; }
        public string Compound { get; set; } = null!;

        public virtual ICollection<Characteristic> Characteristics { get; set; }
    }
}
