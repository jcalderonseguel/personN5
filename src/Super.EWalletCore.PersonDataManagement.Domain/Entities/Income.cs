using System;
using System.Collections.Generic;

namespace Super.EWalletCore.PersonDataManagement.Domain.Entities
{
    public partial class Income
    {
        public long Id { get; set; }
        public decimal? Value { get; set; }
        public Currency? Currency { get; set; }
        public string Company { get; set; }
        public Periodicity? Periodicity { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public long? PersonNumber { get; set; }

        public virtual NaturalPerson PersonNumberNavigation { get; set; }
    }
}
