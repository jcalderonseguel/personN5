using GenFu;
using Super.EWalletCore.PersonDataManagement.Domain.Entities;
using Super.EWalletCore.PersonDataManagement.MockService.Fillers;
using System;
using System.Collections.Generic;

namespace Super.EWalletCore.PersonDataManagement.MockService.Builders
{
    public class PersonMock
    {
        PersonCategory? _personCategory;
        public PersonMock()
        {
            //List<IdentificationDocument> documents = new List<IdentificationDocument>();
            //List<Address> addresses = new List<IdentificationDocument>();
            A.Configure<Person>().Fill(x => x.PersonNumber, 0);
            if (_personCategory == null)
                A.Configure<Person>().Fill(x => x.Category).WithRandom(new PersonCategory[] { PersonCategory.Legal, PersonCategory.Natural });
            else A.Configure<Person>().Fill(x => x.Category, _personCategory);
            A.Configure<Person>().Fill(x => x.RoleId).WithinRange(2, 4);
            A.Configure<Person>().Fill(x => x.IdentificationDocument, new List<IdentificationDocument>());
            A.Configure<Person>().Fill(x => x.Attachment, new List<Attachment>());
            A.Configure<Person>().Fill(x => x.Address, new List<Address>());
            A.Configure<Person>().Fill(x => x.Email, new List<Email>());
            A.Configure<Person>().Fill(x => x.Phone, new List<Phone>());
            if (_personCategory == PersonCategory.Legal)
            A.Configure<Person>().Fill(x => x.PersonLegalPerson, new LegalPerson());
            else
            A.Configure<Person>().Fill(x => x.PersonNaturalPerson, new NaturalPerson());
        }

        private PersonCategory GetRandomCategory(PersonCategory? personCategory)
        {
            Array values = Enum.GetValues(typeof(PersonCategory));
            Random random = new Random();
                return (PersonCategory)values.GetValue(random.Next(values.Length));
        }

        public Person CreatePerson(PersonCategory? personCategory)
        {
            if (personCategory == null)
                _personCategory = GetRandomCategory(personCategory);
            else
            _personCategory = personCategory;            
            return A.New(new Person());
        }

        public IEnumerable<Person> CreatePersonList(int count, PersonCategory? personCategory)
        {
            if (personCategory == null)
                _personCategory = GetRandomCategory(personCategory);
            else
                _personCategory = personCategory;
            return A.ListOf<Person>(count);
        }
    }
}
