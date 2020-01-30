using GenFu;
using Super.EWalletCore.PersonDataManagement.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Super.EWalletCore.PersonDataManagement.MockService.Builders
{
    public class NaturalPersonMock
    {
        public NaturalPersonMock()
        {
            A.Configure<NaturalPerson>().Fill(x => x.FirstName).AsFirstName();
            A.Configure<NaturalPerson>().Fill(x => x.LastNamePrefix).AsPersonTitle();
            A.Configure<NaturalPerson>().Fill(x => x.LastName).AsLastName();
            A.Configure<NaturalPerson>().Fill(x => x.BirthDate, DateTime.Now.AddYears(new Random().Next(-5, 0)));
            A.Configure<NaturalPerson>().Fill(x => x.DateOfDeath).WithRandom(new List<DateTime?> { DateTime.Now.AddYears(new Random().Next(10, 50)), null });
            A.Configure<NaturalPerson>().Fill(x => x.MaritalStatus).WithRandom(new MaritalStatus?[] { MaritalStatus.Single, MaritalStatus.Married, MaritalStatus.Separated, MaritalStatus.Widowed });
            A.Configure<NaturalPerson>().Fill(x => x.Nationality).WithRandom(new Nationality?[] { Nationality.Argentinian, Nationality.Brazilian, Nationality.Chilean });
            A.Configure<NaturalPerson>().Fill(x => x.GenderId).WithinRange(1, 2);

            A.Configure<NaturalPerson>().Fill(x => x.PersonNumber,0);
        }

        public NaturalPerson CreateNaturalPerson()
        {
            return A.New(new NaturalPerson());
        }

        public IEnumerable<NaturalPerson> CreateNaturalPersonList(int count)
        {
            return A.ListOf<NaturalPerson>(count);
        }
    }
}
