using System.Threading.Tasks;
using Super.EWalletCore.PersonDataManagement.Domain.Repositories;

namespace Super.EWalletCore.PersonDataManagement.Persistance
{
    public class ClientDbContextUnitOfWork : IUnitOfWork
    {
        private readonly ClientDbContext context;

        public ClientDbContextUnitOfWork(ClientDbContext context)
        {
            this.context = context;
        }

        public async Task SaveAsync()
        {
            await this.context.SaveChangesAsync();
        }
    }
}
