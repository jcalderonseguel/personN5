using System;
using System.Collections.Generic;

namespace Super.EWalletCore.PersonDataManagement.Domain.Entities
{
    public partial class Country
    {
        public Country()
        {
            IdentificationDocumentType = new HashSet<IdentificationDocumentType>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<IdentificationDocumentType> IdentificationDocumentType { get; private set; }
    }
}
