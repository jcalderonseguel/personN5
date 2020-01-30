using System;
using System.Collections.Generic;

namespace Super.EWalletCore.PersonDataManagement.Domain.Entities
{
    public partial class Phone
    {
        public long Id { get; set; }
        public string CountryCode { get; set; }
        public string AreaCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Extension { get; set; }
        public PhoneType PhoneType { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public long PersonNumber { get; set; }

        public virtual Person PersonNumberNavigation { get; set; }
    }
}
