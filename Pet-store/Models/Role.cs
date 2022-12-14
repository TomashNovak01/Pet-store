using System;
using System.Collections.Generic;

namespace Pet_store.Models
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; }

        public const int ROLE_GUEST = 4;
        public const int ROLE_CUSTOMER = 5;
        public const int ROLE_EMPLOYEE = 6;
        public const int ROLE_ADMINISTRATOR = 7;

        public override string ToString() => Name;
    }
}
