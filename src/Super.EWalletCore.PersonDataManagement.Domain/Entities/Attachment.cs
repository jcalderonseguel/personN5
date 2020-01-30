using System;
using System.Collections.Generic;

namespace Super.EWalletCore.PersonDataManagement.Domain.Entities
{
    public partial class Attachment
    {
        public long Id { get; set; }
        public string FileName { get; set; }
        public string Notes { get; set; }
        public AttachmentType Type { get; set; }
        public string OwnerKey { get; set; }
        public int FileSize { get; set; }
        public string Name { get; set; }
        public string EncodedKey { get; set; }
        public string Location { get; set; }
        public long? IdentificationDocumentId { get; set; }
        public long PersonNumber { get; set; }

        public virtual IdentificationDocument IdentificationDocument { get; set; }
        public virtual Person PersonNumberNavigation { get; set; }
    }
}
