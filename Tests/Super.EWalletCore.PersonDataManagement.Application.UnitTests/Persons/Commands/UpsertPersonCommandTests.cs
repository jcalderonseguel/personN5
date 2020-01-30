using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Moq;
using Shouldly;
using Super.EWalletCore.PersonDataManagement.Application.Mediators.PersonOperations.Insert;
using Super.EWalletCore.PersonDataManagement.Application.UnitTests.Common;
using Super.EWalletCore.PersonDataManagement.Domain.Entities;
using Super.EWalletCore.PersonDataManagement.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Super.EWalletCore.PersonDataManagement.Application.UnitTests.Values.Commands
{
    public class UpsertPersonCommandTests : CommandTestBase
    {
        [Fact]
        public async Task Handle_GivenValidRequest_ShouldRaiseValueCreatedNotification()
        {
            // Arrange
            //long newValueId = 4;

            Income income = new Income
            {
                Value = 1,
                Currency = Currency.Dolar,
                ValidFrom = Convert.ToDateTime("2018-01-01"),
                Periodicity = Periodicity.Days
            };

            Address address = new Address
            {
                 Country="Chile",
                 Region="Bío-Bío",
                 City="Concepción",
                 PostCode="876",
                 StreetName="Cochrane",
                 BuildingNumber="Lagash",
                 AddressLine="Lagash",
                 Latitude=1.4,
                 Longitude=1.4,
                 AddressType= AddressType.Work,
                 PostOfficeBoxCode="Correos",
                 PoboxPostalCode = "100",
                 StatusCodeAddress= StatusCodeAddress.Principal,
                 Coname="Correo",
                 ValidFrom= Convert.ToDateTime("2019-01-01"),
                 ValidTo= Convert.ToDateTime("2019-09-01")

            };

            Phone phone = new Phone
            {
                CountryCode = "+56",
                AreaCode = "+56",
                PhoneNumber = "999999999",
                PhoneType = PhoneType.Work,
                ValidFrom = Convert.ToDateTime("2018-01-01"),
                Extension = "extension"
            };

            Email email = new Email
            {
                EmailAddress = "example@gmail.com",
                ValidFrom = Convert.ToDateTime("2019-01-01")
            };

            NaturalPerson naturalPerson = new NaturalPerson
            {
                FirstName = "Artour",
                LastName = "Garaev",
                LastNamePrefix = "",
                GenderId = 1,
                MaritalStatus = (MaritalStatus)MaritalStatus.Separated,
                Nationality = (Nationality)Nationality.Argentinian,
                BirthDate = DateTime.Now,
                DateOfDeath = null,
            };

            naturalPerson.Income.Add(income);


            var person = new InsertPerson
            {
                Category = PersonCategory.Natural,
                RoleId = 1,
                NaturalPerson = naturalPerson
            };

            person.Address = new List<Address> () {address };
            person.Phone = new List<Phone>() { phone };
            person.Email = new List<Email>() { email };
            person.Attachment = new List<Attachment>();
           

            IdentificationDocument idDocument = new IdentificationDocument
            {
                DocumentNumber = "14707626-6",
                ExpiryDate = DateTime.Now,
                ValidFrom = DateTime.Now,
                ValidTo = DateTime.Now,
                IdentificationDocumentTypeId = 1,
                PersonNumber = 0
            };
           
            person.IdentificationDocument = new List<IdentificationDocument>() { idDocument };

            Mock<IPersonDataRepository> mockRepository = new Mock<IPersonDataRepository>();
            //Mock<IValidator<Person>> mockValidator = new Mock<IValidator<Person>>();
            //Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();

            //mockValidator.Setup(mock => mock.ValidateAsync(person, It.IsAny<CancellationToken>())).ReturnsAsync(new ValidationResult());
            //mockRepository.Setup(mock => mock.InsertAsync(It.IsAny<Person>())).Returns(Task.CompletedTask);
            //mockUnitOfWork.Setup(mock => mock.SaveAsync()).Returns(Task.CompletedTask);

            //var mediatorWork = new Mock<IMediator>();
            var sut = new InsertHandler(_dbContext);
            var result = await sut.Handle(person, CancellationToken.None);
                      
            // Assert
            result.ShouldNotBeNull();

            //mockValidator.Verify(
            //    validator => validator.ValidateAsync(person, It.IsAny<CancellationToken>()),
            //    Times.Once());
            //mockRepository.Verify(
            //    repo => repo.InsertAsync(It.IsAny<Person>()),
            //    Times.Once());
            //mockUnitOfWork.Verify(
            //    repo => repo.SaveAsync(),
            //    Times.Once());
        }
    }
}
