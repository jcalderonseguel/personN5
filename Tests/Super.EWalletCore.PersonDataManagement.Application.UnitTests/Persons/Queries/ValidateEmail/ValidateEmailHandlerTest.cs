using Moq;
using Refit;
using Shouldly;
using Super.EWalletCore.AccountManagement.ApiClients;
using Super.EWalletCore.PersonDataManagement.ApiClients.Dtos;
using Super.EWalletCore.PersonDataManagement.Application.Mediators.PersonOperations.ValidateEmail;
using Super.EWalletCore.PersonDataManagement.Application.Notifications;
using Super.EWalletCore.PersonDataManagement.Application.Queries;
using Super.EWalletCore.PersonDataManagement.Application.UnitTests.Common;
using Super.EWalletCore.PersonDataManagement.Domain.Repositories;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Super.EWalletCore.PersonDataManagement.Application.UnitTests.Persons.Queries.ValidateEmail
{
    public class ValidateEmailHandlerTest
    {
        protected readonly IPersonQuery _personQuery;

        private readonly Mock<IPersonQuery> _personQueryMock = new Mock<IPersonQuery>();
        private readonly Mock<IAccountService> _accountServiceMock = new Mock<IAccountService>();
        private readonly ValidateEmailHandler _handler;

        public ValidateEmailHandlerTest()
        {
            _handler = new ValidateEmailHandler(_personQueryMock.Object, _accountServiceMock.Object);
        }

        [Fact]
        public async Task Should_be_invalid_when_email_is_missing_in_request()
        {
            _personQueryMock.Setup(mock => mock.GetPersonByEmailAndCountryAsync(It.IsAny<string>()))
                .ReturnsAsync(new ValidateEmailDto() { PersonID = 1 });

            var result = await _handler.Handle(new ValidateEmailRequest(null), CancellationToken.None);

            result.ShouldBeOfType<EntityResult<ValidateEmailDto>>();

            result.Entity.ShouldBe(null);
        }

        [Fact]
        public async Task Should_Return_invalid_when_any_format_is_invalid()
        {
            _personQueryMock.Setup(mock => mock.GetPersonByEmailAndCountryAsync(It.IsAny<string>()))
                .ReturnsAsync(new ValidateEmailDto() { PersonID = 1 });

            var result = await _handler.Handle(new ValidateEmailRequest("email"), CancellationToken.None);

            result.ShouldBeOfType<EntityResult<ValidateEmailDto>>();

            result.Entity.ShouldBe(null);
        }

        [Fact]
        public async Task Should_return_not_Found_when_person_doesnt_exist()
        {
            ValidateEmailDto personResult = null;

            _personQueryMock.Setup(mock => mock.GetPersonByEmailAndCountryAsync(It.IsAny<string>()))
                .ReturnsAsync(personResult);

            var result = await _handler.Handle(new ValidateEmailRequest("email@email.com"), CancellationToken.None);

            result.ShouldBeOfType<EntityResult<ValidateEmailDto>>();

            result.Entity.ShouldBeNull();
            result.Error.ShouldBe(ErrorCode.NotFound);
        }

        [Fact]
        public async Task Should_return_not_Found_when_account_service_return_not_found()
        {
            _personQueryMock.Setup(mock => mock.GetPersonByEmailAndCountryAsync(It.IsAny<string>()))
                .ReturnsAsync(new ValidateEmailDto() { PersonID = 1 });

            _accountServiceMock.Setup(mock => mock.ValidateActiveAccountAsync(It.IsAny<long>()))
                .ThrowsAsync(new CustomApiException(null, null, HttpStatusCode.NotFound, null, null));

            var result = await _handler.Handle(new ValidateEmailRequest("email@email.com"), CancellationToken.None);

            result.ShouldBeOfType<EntityResult<ValidateEmailDto>>();

            result.Entity.ShouldBeNull();
            result.Error.ShouldBe(ErrorCode.NotFound);
        }

        [Fact]
        public async Task Should_return_not_Found_when_the_person_does_not_have_an_active_account()
        {
            _personQueryMock.Setup(mock => mock.GetPersonByEmailAndCountryAsync(It.IsAny<string>()))
                .ReturnsAsync(new ValidateEmailDto() { PersonID = 1 });

            _accountServiceMock.Setup(mock => mock.ValidateActiveAccountAsync(It.IsAny<long>()))
                .ReturnsAsync(new ValidateActiveAccountDto() { HasActiveAccount = false });

            var result = await _handler.Handle(new ValidateEmailRequest("email@email.com"), CancellationToken.None);

            result.ShouldBeOfType<EntityResult<ValidateEmailDto>>();

            result.Entity.ShouldBeNull();
            result.Error.ShouldBe(ErrorCode.NotFound);
        }

        [Fact]
        public async Task Should_OK_when_person_exist()
        {
            _personQueryMock.Setup(mock => mock.GetPersonByEmailAndCountryAsync(It.IsAny<string>()))
                .ReturnsAsync(new ValidateEmailDto() { PersonID = 1 });

            _accountServiceMock.Setup(mock => mock.ValidateActiveAccountAsync(It.IsAny<long>()))
                .ReturnsAsync(new ValidateActiveAccountDto() { HasActiveAccount = true });

            var result = await _handler.Handle(new ValidateEmailRequest("email@email.com"), CancellationToken.None);

            result.ShouldBeOfType<EntityResult<ValidateEmailDto>>();

            result.Entity.PersonID.ShouldBe(1);
        }
    }
}
