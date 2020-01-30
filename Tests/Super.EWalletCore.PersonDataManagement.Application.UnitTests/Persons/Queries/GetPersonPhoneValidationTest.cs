using Moq;
using Shouldly;
using Super.EWalletCore.AccountManagement.ApiClients;
using Super.EWalletCore.PersonDataManagement.Application.Mediators.PersonOperations.ValidatePhoneNumber;
using Super.EWalletCore.PersonDataManagement.Application.Mediators.ValidatePhoneNumber.Query;
using Super.EWalletCore.PersonDataManagement.Application.UnitTests.Common;
using Super.EWalletCore.PersonDataManagement.Domain.Entities;
using Super.EWalletCore.PersonDataManagement.Domain.Repositories;
using Super.EWalletCore.PersonDataManagement.Persistance;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Super.EWalletCore.PersonDataManagement.Application.UnitTests.Persons.Queries
{
    [Collection("QueryCollection")]
    public class GetPersonPhoneValidationTest
    {
        private readonly ClientDbContext context;
        protected readonly IPersonQuery personQuery;
        private readonly Mock<IPersonQuery> _personQueryMock = new Mock<IPersonQuery>();
        private readonly Mock<IAccountService> _accountService = new Mock<IAccountService>();

        private readonly ValidatePhoneNumberHandler handler;

        public GetPersonPhoneValidationTest(QueryTestFixture fixture)
        {
            this.context = fixture.Context;
            handler = new ValidatePhoneNumberHandler(_personQueryMock.Object,_accountService.Object );
        }

        [Fact]
        public async Task GetValidatePersonPhone_Success()
        {
            _accountService.Setup(x => x.ValidateActiveAccountAsync(It.IsAny<long>())).ReturnsAsync(new ApiClients.Dtos.ValidateActiveAccountDto() { HasActiveAccount = true });
            _personQueryMock.Setup(mock => mock.GetPersonByPhoneNumberAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new ValidatePhoneNumberDto() { PersonID = 1 });

            var result = await handler.Handle(new ValidatePhoneNumberRequest("56", "56", "999999999"), CancellationToken.None);

            result.Entity.PersonID.ShouldBe(1);
        }

        [Fact]
        public async Task GetValidatePersonPhone_Error()
        {
            var result = await handler.Handle(new ValidatePhoneNumberRequest("11", "54", ""), CancellationToken.None);

            result.Invalid.ShouldBeTrue();
        }


        [Fact]
        public async Task GetValidatePersonPhone_Invalid_Error()
        {
            var result = await handler.Handle(new ValidatePhoneNumberRequest("11", "dasdasdas", ""), CancellationToken.None);

            result.Invalid.ShouldBeTrue();
        }
    }
}
