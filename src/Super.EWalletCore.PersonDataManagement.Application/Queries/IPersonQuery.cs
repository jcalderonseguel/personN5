using Super.EWalletCore.PersonDataManagement.Application.Mediators.PersonOperations.ValidatePhoneNumber;
using Super.EWalletCore.PersonDataManagement.Application.Queries;
using Super.EWalletCore.PersonDataManagement.Domain.Entities;
using System.Threading.Tasks;

namespace Super.EWalletCore.PersonDataManagement.Domain.Repositories
{
    public interface IPersonQuery
    {
        Task<ValidateEmailDto> GetPersonByEmailAndCountryAsync(string email);
        Task<ValidatePhoneNumberDto> GetPersonByPhoneNumberAsync(string countryCode, string areaCode, string phoneNumber);
    }
}