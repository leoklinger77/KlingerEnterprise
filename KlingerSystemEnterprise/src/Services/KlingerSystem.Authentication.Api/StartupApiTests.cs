using KlingerSystem.Authentication.Api.Configuration;
using KlingerSystem.Authentication.Api.Data;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KlingerSystem.Authentication.Api
{
    public class StartupApiTests
    {
        public IConfiguration Configuration { get; }
        public StartupApiTests(IHostEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            if (hostEnvironment.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup));            

            services.AddDbContext<AuthenticationDbContext>(option => option.UseInMemoryDatabase(databaseName: "InMemoryDb"));

            services.ApiConfig(Configuration);
            services.IdentityConfig(Configuration);            
            services.RegisterDependencyInjection();                        
            services.AddMessageBusConfiguration(Configuration);            
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            app.ApiAppConfig(env);            
        }
    }
}
