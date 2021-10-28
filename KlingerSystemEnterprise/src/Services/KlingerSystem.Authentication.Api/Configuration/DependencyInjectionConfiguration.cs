using KlingerSystem.Api.Users;
using KlingerSystem.Authentication.Api.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace KlingerSystem.Authentication.Api.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void RegisterDependencyInjection(this IServiceCollection services)
        {
            //Services
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            services.AddScoped<GeneretorToken>();
            //services.AddScoped<IEmailSender>();
        }
    }
}
