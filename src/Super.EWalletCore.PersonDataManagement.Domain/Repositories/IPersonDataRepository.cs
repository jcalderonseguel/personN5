using Super.EWalletCore.PersonDataManagement.Domain.Entities;
using System.Threading.Tasks;

namespace Super.EWalletCore.PersonDataManagement.Domain.Repositories
{
   public interface IPersonDataRepository
    {
        Task InsertAsync(Person personData);

        Task<Person> GetPersonByDocumentNumber(int genderId, int countryId, int identificationDocumentTypeId, string documentNumber);

        IdentificationDocument GetDocumentNumber(string DocumentNumber);

        IdentificationDocumentType GetIdentificationDocumentTypeById(long id);

        Role GetRoleById(int id);

        Gender GetGenderById(int id);
    }
}
