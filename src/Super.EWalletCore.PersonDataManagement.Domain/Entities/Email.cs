using System;
using System.Collections.Generic;

namespace Super.EWalletCore.PersonDataManagement.Domain.Entities
{
    public partial class Email
    {
        public long Id { get; set; }
        public string EmailAddress { get; set; }
        public bool? Validated { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public long? PersonNumber { get; set; }

        public virtual Person PersonNumberNavigation { get; set; }
    }
}
