using Microsoft.Extensions.Configuration;
using Refit;
using Super.EWalletCore.AccountManagement.ApiClients;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AccountServiceCollectionExtensions
    {
        private static void AddAccountServiceClient(this IServiceCollection services, string url)
        {
            services.AddRefitClient<IAccountService>()
                    .ConfigureHttpClient(client => client.BaseAddress = new Uri(url));
        }
        public static void AddAccountServiceClient(this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration.GetSection("AccountServiceUrl").Value;
            AddAccountServiceClient(services, url);
        }
    }
}
