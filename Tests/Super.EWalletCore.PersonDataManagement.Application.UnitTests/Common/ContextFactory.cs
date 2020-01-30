using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Super.EWalletCore.PersonDataManagement.Domain.Entities;
using Super.EWalletCore.PersonDataManagement.MockService.Builders;
using Super.EWalletCore.PersonDataManagement.Persistance;
using System;
using System.Collections.Generic;
using System.Text;

namespace Super.EWalletCore.PersonDataManagement.Application.UnitTests.Common
{
    public class ContextFactory
    {
        public static ClientDbContext Create()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<ClientDbContext>()
                    .UseSqlite(connection)
                    .Options;

            var dbContext = new ClientDbContext(options);

            dbContext.Database.EnsureCreated();

            dbContext.Role.AddRange(
                new RoleMock().CreateRoleList());
            dbContext.Gender.AddRange(new GenderMock().CreateGendersList());
            dbContext.Country.AddRange(new CountryMock().CreateCountriesList());
            dbContext.IdentificationDocumentType.AddRange(new IdentificationDocumentTypeMock().CreateIdentificationDocumentTypeList());

            //Income[] incomes = new Income[1];

            //incomes[0] = new Income
            //{
            //    Value = 1,
            //    Currency = Currency.Dolar,
            //    ValidFrom = DateTime.Now
            //};

            //Address[] addresses = new Address[1];

            //addresses[0] = new Address
            //{
            //    Country = "Chile",
            //    PostCode = "BDE87",
            //    AddressType = AddressType.Work,
            //    ValidFrom = DateTime.Now,
            //    Coname = "coname"
            //};

            //Phone[] phones = new Phone[1];
            //phones[0] = new Phone
            //{
            //    CountryCode = "+56",
            //    AreaCode = "+56",
            //    PhoneNumber = "999999999",
            //    PhoneType = PhoneType.Work,
            //    ValidFrom = DateTime.Now,
            //    Extension = "extension"
            //};

            //Email[] emails = new Email[1];
            //emails[0] = new Email
            //{
            //    EmailAddress = "example@gmail.com",
            //    ValidFrom = DateTime.Now
            //};

            var person = new Person
            {
                Category = PersonCategory.Natural,
                RoleId = 1,
                PersonNaturalPerson = new NaturalPerson
                {
                    FirstName = "Artur",
                    LastName = "Garaev",
                    GenderId = 1,
                    BirthDate = DateTime.Now,
                    //Income = incomes,
                },
                //Address = addresses,
                //Phone = phones,
                //Email = emails
            };
            IdentificationDocument idDocument = new IdentificationDocument
            {
                DocumentNumber = "88888888-1",
                ValidFrom = DateTime.Now,
                ValidTo = DateTime.Now.AddYears(5),
                IdentificationDocumentTypeId = 1
            };
            person.IdentificationDocument.Add(idDocument);
            dbContext.Persons.Add(person);

            person = new Person
            {
                Category = PersonCategory.Natural,
                RoleId = 1,
                PersonNaturalPerson = new NaturalPerson
                {
                    FirstName = "Diego",
                    LastName = "Calzadilla",
                    GenderId = 1,
                    BirthDate = DateTime.Now,
                }
            };
            idDocument = new IdentificationDocument
            {
                DocumentNumber = "33333333-1",
                ValidFrom = DateTime.Now,
                ValidTo = DateTime.Now.AddYears(5),
                IdentificationDocumentTypeId = 1
            };
            person.IdentificationDocument.Add(idDocument);
            dbContext.Persons.Add(person);

            person = new Person
            {
                Category = PersonCategory.Natural,
                RoleId = 1,
                PersonNaturalPerson = new NaturalPerson
                {
                    FirstName = "Test",
                    LastName = "Test",
                    GenderId = 1,
                    BirthDate = DateTime.Now
                }
            };
            idDocument = new IdentificationDocument
            {
                DocumentNumber = "11111111-1",
                ValidFrom = DateTime.Now,
                ValidTo = DateTime.Now.AddYears(5),
                IdentificationDocumentTypeId = 1
            };
            person.IdentificationDocument.Add(idDocument);
            dbContext.Persons.Add(person);

            dbContext.SaveChanges();

            return dbContext;
        }

        public static void Destroy(ClientDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
