using SqlKata.Compilers;
using SqlKata.Execution;
using Super.EWalletCore.PersonDataManagement.Application.Mediators.PersonOperations.ValidatePhoneNumber;
using Super.EWalletCore.PersonDataManagement.Application.Queries;
using Super.EWalletCore.PersonDataManagement.Domain.Entities;
using Super.EWalletCore.PersonDataManagement.Domain.Repositories;
using System.Data;
using System.Threading.Tasks;

namespace Super.EWalletCore.PersonDataManagement.Persistance.Queries
{
    public class PersonQuery : IPersonQuery
    {
        private readonly IDbConnection connection;
        private readonly Compiler sqlKataCompiler;

        public PersonQuery(IDbConnection connection, Compiler sqlKataCompiler)
        {
            this.connection = connection;
            this.sqlKataCompiler = sqlKataCompiler;
        }

        public Task<ValidateEmailDto> GetPersonByEmailAndCountryAsync(string email)
        {
            var query = new QueryFactory(connection, sqlKataCompiler).Query("Persons")
                .Select("Persons.PersonNumber as PersonID")
                .Join("Email","Email.PersonNumber","Persons.PersonNumber","=")
                .When(email != null, q => q.Where("Email.EmailAddress", "=", email));

         

            return query.FirstOrDefaultAsync<ValidateEmailDto>();
        }

        public Task<ValidatePhoneNumberDto> GetPersonByPhoneNumberAsync(string countryCode, string areaCode, string phoneNumber)
        {
            var query = new QueryFactory(connection, sqlKataCompiler).Query("Persons")
                .Select("Persons.PersonNumber as PersonID")
                .Join("Phone", "Phone.PersonNumber", "Persons.PersonNumber", "=")
                .When(countryCode != null, q => q.Where("Phone.CountryCode", "=", countryCode))
                .When(areaCode != null, q => q.Where("Phone.AreaCode", "=", areaCode))
                .When(phoneNumber != null, q => q.Where("Phone.PhoneNumber", "=", phoneNumber));

            return query.FirstOrDefaultAsync<ValidatePhoneNumberDto>();
        }
    }
}
