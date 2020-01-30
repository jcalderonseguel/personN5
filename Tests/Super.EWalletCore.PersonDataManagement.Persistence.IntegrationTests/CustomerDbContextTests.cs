using Microsoft.EntityFrameworkCore;
using Super.EWalletCore.PersonDataManagement.Persistance;
using System;
using Xunit;
using Shouldly;
using Super.EWalletCore.PersonDataManagement.Domain.Entities;
using System.Threading.Tasks;

namespace Super.EWalletCore.PersonDataManagement.Persistence.IntegrationTests
{
    public class CustomerDbContextTests
    {
        private readonly ClientDbContext _sut;

        public CustomerDbContextTests()
        {
            var options = new DbContextOptionsBuilder<ClientDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _sut = new ClientDbContext(options);

            var person = new Person
            {
                Category = PersonCategory.Natural,
                RoleId = 1,
                PersonNaturalPerson = new NaturalPerson
                {
                    FirstName = "Artour",
                    LastName = "Garaev",
                    GenderId = 1
                }
            };
            IdentificationDocument idDocument = new IdentificationDocument
            {
                DocumentNumber = "14707626-6",
                ValidFrom = DateTime.Now,
                ValidTo = DateTime.Now.AddYears(5)
            };
            person.IdentificationDocument.Add(idDocument);
            _sut.Persons.Add(person);
             
            _sut.SaveChanges();
        }

        [Fact]
        public async Task SaveChangesAsync_GivenNewPerson_ShouldSetCreatedProperties()
        {
            var testData = new Person
            {
                Category = PersonCategory.Natural,
                RoleId = 1,
                PersonNaturalPerson = new NaturalPerson
                {
                    FirstName = "Hernan",
                    LastName = "Candia",
                    GenderId = 1
                }
            };

            _sut.Persons.Add(testData);

            await _sut.SaveChangesAsync();

            testData.PersonNaturalPerson.FirstName.ShouldBe("Hernan");
            testData.PersonNaturalPerson.LastName.ShouldBe("Candia");
            testData.PersonNaturalPerson.Gender.ShouldBe(_sut.Gender.Find(testData.PersonNaturalPerson.GenderId));
        }

        [Fact]
        public async Task SaveChangesAsync_GivenNewNaturalPerson_ShouldSetCorrectPersonCategory()
        {
            var testData = new Person
            {
                Category = PersonCategory.Natural,
                RoleId = 1,
                PersonNaturalPerson = new NaturalPerson
                {
                    FirstName = "Hernan",
                    LastName = "Candia",
                    GenderId = 1
                }
            };

            _sut.Persons.Add(testData);

            await _sut.SaveChangesAsync();

            testData.Category.ShouldBe(PersonCategory.Natural);
        }

        [Fact]
        public async Task SaveChangesAsync_GivenExistingNaturalPerson_ShouldSetLastModifiedProperties()
        {
            var dataTest = await _sut.Persons.FindAsync((long)1);

            dataTest.PersonNaturalPerson.FirstName = "Javier";
            dataTest.PersonNaturalPerson.LastName = "Calderon";
            dataTest.PersonNaturalPerson.GenderId = 2;

            await _sut.SaveChangesAsync();

            dataTest.PersonNaturalPerson.FirstName.ShouldBe("Javier");
            dataTest.PersonNaturalPerson.LastName.ShouldBe("Calderon");
            dataTest.PersonNaturalPerson.GenderId.ShouldNotBe(1);
        }

        [Fact]
        public void Dispose()
        {
            _sut?.Dispose();
        }
    }
}
