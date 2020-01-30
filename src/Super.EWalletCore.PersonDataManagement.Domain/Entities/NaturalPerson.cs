using System;
using System.Collections.Generic;

namespace Super.EWalletCore.PersonDataManagement.Domain.Entities
{
    public partial class NaturalPerson 
    {
        public NaturalPerson()
        {
            Income = new HashSet<Income>();
        }

        public string FirstName { get; set; }
        public string LastNamePrefix { get; set; }
        public string LastName { get; set; }
        // public string FullName { get; set; }

        public string FullName => $"{FirstName} {LastName}";
        public DateTime? BirthDate { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public MaritalStatus? MaritalStatus { get; set; }
        public Nationality? Nationality { get; set; }
        public int GenderId { get; set; }
        public long PersonNumber { get; set; }

        public virtual Gender Gender { get; set; }
        public virtual Person PersonNumberNavigation { get; set; }
        public virtual ICollection<Income> Income { get; set; }
    }
}
