
using System;
using System.Collections.Generic;

namespace Super.EWalletCore.PersonDataManagement.Domain.Entities
{
    public partial class Person 
    {
        public Person()
        {
            Address = new HashSet<Address>();
            Attachment = new HashSet<Attachment>();
            Email = new HashSet<Email>();
            IdentificationDocument = new HashSet<IdentificationDocument>();
            Phone = new HashSet<Phone>();
        }

        public long PersonNumber { get; set; }
        public PersonCategory Category { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual LegalPerson PersonLegalPerson { get; set; }
        public virtual NaturalPerson PersonNaturalPerson { get; set; }
        public virtual ICollection<Address> Address { get; set; }
        public virtual ICollection<Attachment> Attachment { get; set; }
        public virtual ICollection<Email> Email { get; set; }
        public virtual ICollection<IdentificationDocument> IdentificationDocument { get; set; }
        public virtual ICollection<Phone> Phone { get; set; }
    }
}
