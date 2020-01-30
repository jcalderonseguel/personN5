using Microsoft.EntityFrameworkCore;
using Super.EWalletCore.PersonDataManagement.Domain.Entities;
using Super.EWalletCore.PersonDataManagement.Domain.Repositories;
using System.Linq;
using System.Threading.Tasks;
namespace Super.EWalletCore.PersonDataManagement.Persistance.Commands
{
    public class PersonDataRepository : IPersonDataRepository
    {
        private readonly ClientDbContext context;

        public PersonDataRepository(ClientDbContext context)
        {
            this.context = context;
        }

        public async Task InsertAsync(Person personData)
        {
            await context.Persons.AddAsync(personData);
        }

        public async Task<Person> GetPersonByDocumentNumber(int genderId
           , int countryId, int identificationDocumentTypeId, string documentNumber)
        {
            //var req = request;
            return await context.Persons.Include(x => x.PersonLegalPerson).Include(x => x.PersonNaturalPerson).Where(i => i.PersonNaturalPerson.GenderId == genderId && i.IdentificationDocument.Any(d => d.IdentificationDocumentType.CountryId == countryId && d.IdentificationDocumentTypeId == identificationDocumentTypeId && d.DocumentNumber == documentNumber)).FirstOrDefaultAsync();
        }

        public IdentificationDocumentType GetIdentificationDocumentTypeById(long id)
        {
            var typeDoc = context.IdentificationDocumentType
                                      .Where(s => s.Id == id)
                                      .FirstOrDefault();

            return typeDoc;
        }

        public IdentificationDocument GetDocumentNumber(string DocumentNumber)
        {

            var doc = context.IdentificationDocument
                                      .Where(s => s.DocumentNumber == DocumentNumber)
                                      .FirstOrDefault();

            return doc;
        }

        public Role GetRoleById(int id)
        {
            var role = context.Role.Where(s => s.Id == id).FirstOrDefault();

            return role;
        }

        public Gender GetGenderById(int id)
        {
            var gender = context.Gender.Where(s => s.Id == id).FirstOrDefault();

            return gender;
        }
    }
}
