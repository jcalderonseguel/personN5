
using System;
using System.Collections.Generic;

namespace Super.EWalletCore.PersonDataManagement.Domain.Entities
{
    public partial class LegalPerson 
    {
        public string FullName { get; set; }
        public long PersonNumber { get; set; }

        public virtual Person PersonNumberNavigation { get; set; }
    }
}
