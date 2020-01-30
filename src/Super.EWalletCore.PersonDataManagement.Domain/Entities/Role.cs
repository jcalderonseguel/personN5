using System;
using System.Collections.Generic;

namespace Super.EWalletCore.PersonDataManagement.Domain.Entities
{
    public partial class Role
    {
        public Role()
        {
            Person = new HashSet<Person>();
        }

        public int Id { get; set; }
        public RoleType RoleType { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }

        public virtual ICollection<Person> Person { get; private set; }
    }
}
