using System;
using System.Collections.Generic;

namespace Super.EWalletCore.PersonDataManagement.Domain.Entities
{
    public partial class Gender
    {
        public Gender()
        {
            PersonNaturalPerson = new HashSet<NaturalPerson>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<NaturalPerson> PersonNaturalPerson { get; private set; }
    }
}
