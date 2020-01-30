using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Super.EWalletCore.PersonDataManagement.Application.Common.Interfaces;
using System.Data;
using System.Data.SqlClient;

using SqlKata.Compilers;

namespace Super.EWalletCore.PersonDataManagement.Persistance
{
    public static class DependencyInjection
    {
        //private readonly IConfiguration configuration;

        //public DependencyInjection(IConfiguration configuration)
        //{
        //    this.configuration = configuration;
        //}

        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PersonsDB");

            services.AddTransient<Compiler, SqlServerCompiler>();
            services.AddTransient<IDbConnection>(b =>
            {
                return new SqlConnection(connectionString);
            });

            services.AddDbContext<ClientDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("PersonsDB")));

            services.AddScoped<IClientDbContext>(provider => provider.GetService<ClientDbContext>());

            return services;
        }
    }
}
