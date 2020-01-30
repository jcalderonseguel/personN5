using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Super.EWalletCore.PersonDataManagement.Application.Mediators.PersonOperations.ValidateEmail;


namespace Super.EWalletCore.PersonDataManagement.Bootstrap.Providers
{
    public static class MediatorConfiguration
    {
        public static IServiceCollection ConfigureMediatrServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ValidateEmailHandler).Assembly);
            return services;
        }
    }
}
