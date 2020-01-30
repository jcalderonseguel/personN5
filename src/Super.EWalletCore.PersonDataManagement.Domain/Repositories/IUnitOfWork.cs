using System.Threading.Tasks;

namespace Super.EWalletCore.PersonDataManagement.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
    }
}
