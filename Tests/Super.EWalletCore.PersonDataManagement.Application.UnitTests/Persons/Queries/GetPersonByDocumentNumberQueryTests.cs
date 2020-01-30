using Shouldly;
using Super.EWalletCore.PersonDataManagement.Application.Mediators.PersonOperations.GetPersonByDocumentNumber;
using Super.EWalletCore.PersonDataManagement.Application.UnitTests.Common;
using Super.EWalletCore.PersonDataManagement.Domain.Repositories;
using Super.EWalletCore.PersonDataManagement.Persistance;
using Super.EWalletCore.PersonDataManagement.Persistance.Commands;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Super.EWalletCore.PersonDataManagement.Application.UnitTests.Values.Queries
{
    [Collection("QueryCollection")]
    public class GetPersonByDocumentNumberQueryTests
    {
        private readonly ClientDbContext _context;

   

        public GetPersonByDocumentNumberQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            
        }

        [Fact]
        public async Task GetValueByIdTests()
        {
            var sut = new GetPersonByDocumentNumberQueryHandler(_context);

            var result = await sut.Handle(new GetPersonByDocumentNumberQuery("11111111-1", 1,  1,  1), CancellationToken.None);

            result.ShouldBeOfType<PersonInfoDto>();

            result.PersonNumber.ShouldBe(3);
        }
    }
}
