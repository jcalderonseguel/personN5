using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlKata.Compilers;
using Super.EWalletCore.PersonDataManagement.Application.Common.Interfaces;
using Super.EWalletCore.PersonDataManagement.Domain.Repositories;
using Super.EWalletCore.PersonDataManagement.Persistance;

namespace Super.EWalletCore.PersonDataManagement.Bootstrap.Providers
{
    public class PersistanceConfiguration
    {
        private readonly IConfiguration configuration;

        public PersistanceConfiguration(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = configuration.GetConnectionString("PersonsDB");

            services.AddTransient<Compiler, SqlServerCompiler>();
            services.AddTransient<IDbConnection>(b =>
            {
                return new SqlConnection(connectionString);
            });

            services.AddDbContextPool<ClientDbContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Super.EWalletCore.PersonDataManagement.API")));
            services.AddScoped<IUnitOfWork, ClientDbContextUnitOfWork>();

            services.AddScoped<IClientDbContext>(provider => provider.GetService<ClientDbContext>());
        }
    }
}
