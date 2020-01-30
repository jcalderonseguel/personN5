using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.IO;
using System.Reflection;

namespace Super.EWalletCore.PersonDataManagement.Bootstrap.Providers
{
    public class SwaggerConfiguration
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Person API", Version = "v1" });
                IncludeXMLS(c);
            
            });
        }

        private static void IncludeXMLS(SwaggerGenOptions options)
        {
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var files = Directory.GetFiles(path, "*.xml");
            foreach (var item in files)
                options.IncludeXmlComments(item);

        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {                
                c.SwaggerEndpoint("v1/swagger.json", "Super.EWalletCore.PersonDataManagement");
            });
        }
    }
}