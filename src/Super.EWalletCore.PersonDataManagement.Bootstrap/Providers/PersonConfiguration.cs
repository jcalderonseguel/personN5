using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Super.EWalletCore.PersonDataManagement.Application.Validations;
using Super.EWalletCore.PersonDataManagement.Domain.Repositories;
using Super.EWalletCore.PersonDataManagement.Persistance.Commands;
using Super.EWalletCore.PersonDataManagement.Persistance.Queries;

namespace Super.EWalletCore.PersonDataManagement.Bootstrap.Providers
{
    public class PersonConfiguration
    {
        public void ConfigureServices(IServiceCollection services)
        {
           //services.AddTransient<IPersonDataRepository, PersonDataRepository>();
           services.AddTransient<IPersonQuery, PersonQuery>();
           //services.AddTransient<IValidator<PersonProto>, PersonValidator>();
        }
    }
}
