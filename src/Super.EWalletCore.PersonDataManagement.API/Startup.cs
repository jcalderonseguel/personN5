using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Super.EWalletCore.PersonDataManagement.API.Presenters;
using Super.EWalletCore.PersonDataManagement.API.Presenters.Interfaces;
using Super.EWalletCore.PersonDataManagement.Persistance;
using Super.EWalletCore.PersonDataManagement.Application;
using Super.EWalletCore.PersonDataManagement.Bootstrap;
using Microsoft.AspNetCore.Mvc;
using Super.EWalletCore.PersonDataManagement.Application.Common.Interfaces;
using Super.EWalletCore.PersonDataManagement.API.Common;

namespace Super.EWalletCore.PersonDataManagement.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        private readonly ServiceConfiguration serviceConfiguration;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
            serviceConfiguration = new ServiceConfiguration(configuration);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Go to Bootstrap layer to inject service integration & repositroy
            serviceConfiguration.ConfigureServices(services);
            // Injection to Persistance layer 
            services.AddPersistence(Configuration);
            // Injection to Application layer
            services.AddApplication();



            //services.Configure<ApiBehaviorOptions>(options =>
            //{
            //    options.SuppressModelStateInvalidFilter = true;
            //});

            services.AddTransient<IDocumentTypePresenter, DocumentTypePresenter>();
            services.AddTransient<IPersonPresenter, PersonPresenter>();
            services.AddTransient<IValidateEmailPresenter, ValidateEmailPresenter>();
            services.AddTransient<IValidatePhoneNumberPresenter, ValidatePhoneNumberPresenter>();

            services
            .AddControllersWithViews()
            .AddNewtonsoftJson()
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<IClientDbContext>());


            services.AddHealthChecks().AddDbContextCheck<ClientDbContext>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Person API", Version = "v1" });
            });

            #region GRPC compression
            // GRPC Compression global level
            //services.AddGrpc(o=> 
            //{
            //    o.ResponseCompressionLevel = CompressionLevel.Optimal;
            //    o.ResponseCompressionAlgorithm = "gzip";
            //});


            // GRPC Compression per service
            //services.AddGrpc()
            //    .AddServiceOptions<PersonService>(o =>
            //    {
            //        o.ResponseCompressionLevel = CompressionLevel.Optimal;
            //        o.ResponseCompressionAlgorithm = "gzip";
            //    });
            #endregion

            #region GRPC services, reflection y health status

            // services.AddGrpc();
            //services.AddSingleton<HealthServiceImpl>();
            //services.AddTransient<PersonService>();
            //services.AddHostedService<StatusService>();
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // serviceConfiguration.Configure(app, env.IsDevelopment());

            app.UseHealthChecks("/health");
            app.UseCustomExceptionHandler();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Super.EWalletCore.PersonDataManagement");
            });

           
        }
    }
}
