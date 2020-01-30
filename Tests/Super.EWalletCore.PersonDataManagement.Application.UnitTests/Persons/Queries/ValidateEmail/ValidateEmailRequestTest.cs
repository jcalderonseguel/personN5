using Shouldly;
using Super.EWalletCore.PersonDataManagement.Application.Mediators.PersonOperations.ValidateEmail;
using Xunit;

namespace Super.EWalletCore.PersonDataManagement.Application.UnitTests.Persons.Queries.ValidateEmail
{
    public class ValidateEmailRequestTest
    {
        [Fact]
        public void Should_be_invalid_when_email_is_null()
        {
            var request = new ValidateEmailRequest(null);

            request.Invalid.ShouldBeTrue();
        }

        [Fact]
        public void Should_be_invalid_when_email_forma_is_not_correct()
        {
            var request = new ValidateEmailRequest("email");

            request.Invalid.ShouldBeTrue();
        }

        [Fact]
        public void Should_be_valid_when_all_data_was_provided()
        {
            var request = new ValidateEmailRequest("email@email.com");

            request.Valid.ShouldBeTrue();
        }
    }
}
