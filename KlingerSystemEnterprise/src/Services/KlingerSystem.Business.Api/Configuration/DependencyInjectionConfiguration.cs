using KlingerSystem.Api.Users;
using KlingerSystem.Business.Domain.Interfaces;
using KlingerSystem.Business.Infrastructure.Data;
using KlingerSystem.Business.Infrastructure.Repositorys;
using KlingerSystem.Core.Mediatr;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace KlingerSystem.Business.Api.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void RegisterDependencyInjection(this IServiceCollection services)
        {
            //Services
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();
            services.AddScoped<IMediatrHandler, MediatrHandler>();

            //Repositorys
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<BusinessContext>();

            //Commands
            

            //Events


            //Querys

        }
    }
}
