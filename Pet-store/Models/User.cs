using System;
using System.Collections.Generic;
using System.Linq;

namespace Pet_store.Models
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public int IdRole { get; set; }
        public string Lastname { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateOnly? DateOfBirthday { get; set; }

        public virtual Role IdRoleNavigation { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }

        public string FullName => $"{Lastname} {Name}";
    }
}
