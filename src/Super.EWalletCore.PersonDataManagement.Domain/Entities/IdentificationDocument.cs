using System;
using System.Collections.Generic;

namespace Super.EWalletCore.PersonDataManagement.Domain.Entities
{
    public partial class IdentificationDocument
    {
        public IdentificationDocument()
        {
            Attachment = new HashSet<Attachment>();
        }

        public long Id { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime? IssuingDate { get; set; }
        public string IssuingAuthority { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public long PersonNumber { get; set; }
        public int IdentificationDocumentTypeId { get; set; }

        public virtual IdentificationDocumentType IdentificationDocumentType { get; set; }
        public virtual Person PersonNumberNavigation { get; set; }
        public virtual ICollection<Attachment> Attachment { get; private set; }
    }
}
