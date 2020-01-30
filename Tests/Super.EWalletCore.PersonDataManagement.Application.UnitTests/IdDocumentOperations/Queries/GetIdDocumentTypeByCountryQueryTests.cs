using Shouldly;
using Super.EWalletCore.PersonDataManagement.Application.Common.Exceptions;
using Super.EWalletCore.PersonDataManagement.Application.Mediators.IdDocumentOperations.Queries;
using Super.EWalletCore.PersonDataManagement.Application.UnitTests.Common;
using Super.EWalletCore.PersonDataManagement.Persistance;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Super.EWalletCore.PersonDataManagement.Application.UnitTests.IdDocumentOperations.Queries
{
    [Collection("QueryCollection")]
    public class GetIdDocumentTypeByCountryQueryTests
    {
        private readonly ClientDbContext _context;

        public GetIdDocumentTypeByCountryQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task GetValueByCountryTests()
        {
            var sut = new GetIdDocumentTypeByCountryQueryHandler(_context);
            var result = await sut.Handle(new GetIdDocumentTypeByCountryQuery(1) , CancellationToken.None);

            result.ShouldBeOfType<IdentificationDocumentVm>();

            result.idDocumentTypeList.Count.ShouldBe(2);
        }

        //[Fact]
        //public async Task Handle_GivenInvalidRequest_ThrowsValidationException()
        //{
        //    var sut = new GetIdDocumentTypeByCountryQueryHandler(_context);
        //    int invalidCountryId = 4;
        //    var command = new GetIdDocumentTypeByCountryQuery(invalidCountryId);
        //    await Assert.ThrowsAsync<ValidateException>(() => sut.Handle(command, CancellationToken.None));
        //}
    }
}
