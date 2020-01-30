using Refit;
using Super.EWalletCore.PersonDataManagement.ApiClients.Dtos;
using System.Threading.Tasks;

namespace Super.EWalletCore.AccountManagement.ApiClients
{
    public interface IAccountService
    {
        [Get("/accounts/validateactiveaccount/{personId}")]
        Task<ValidateActiveAccountDto> ValidateActiveAccountAsync(long? personId);
    }
}
