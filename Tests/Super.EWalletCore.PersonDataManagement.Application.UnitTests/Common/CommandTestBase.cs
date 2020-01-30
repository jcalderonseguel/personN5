using Super.EWalletCore.PersonDataManagement.Domain.Repositories;
using Super.EWalletCore.PersonDataManagement.Persistance;
using Super.EWalletCore.PersonDataManagement.Persistance.Commands;
using System;

namespace Super.EWalletCore.PersonDataManagement.Application.UnitTests.Common
{
    public class CommandTestBase : IDisposable
    {
        protected readonly ClientDbContext _dbContext;
        protected readonly IPersonDataRepository _personDataRepository;

        public CommandTestBase()
        {
            _dbContext = ContextFactory.Create();
            _personDataRepository =new PersonDataRepository(_dbContext);
        }

        public void Dispose()
        {
            ContextFactory.Destroy(_dbContext);
        }
    }
}
