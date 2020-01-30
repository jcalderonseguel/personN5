using AutoMapper;
using Super.EWalletCore.PersonDataManagement.Persistance;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Super.EWalletCore.PersonDataManagement.Application.UnitTests.Common
{
    public class QueryTestFixture : IDisposable
    {
        public ClientDbContext Context { get; private set; }
        public QueryTestFixture()
        {
            Context = ContextFactory.Create();
        }

        public void Dispose()
        {
            ContextFactory.Destroy(Context);
        }
        [CollectionDefinition("QueryCollection")]
        public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
    }
}
