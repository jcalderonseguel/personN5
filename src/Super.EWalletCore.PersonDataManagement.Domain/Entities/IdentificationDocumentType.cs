using System;
using System.Collections.Generic;

namespace Super.EWalletCore.PersonDataManagement.Domain.Entities
{
    public partial class IdentificationDocumentType
    {
        public IdentificationDocumentType()
        {
            IdentificationDocument = new HashSet<IdentificationDocument>();
        }

        public int Id { get; set; }
        public string IdType { get; set; }
        public string Description { get; set; }
        public int CountryId { get; set; }
        public bool? CheckDigit { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<IdentificationDocument> IdentificationDocument { get; private set; }
    }
}
