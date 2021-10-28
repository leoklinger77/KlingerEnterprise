using KlingerSystem.Api.Identity;
using KlingerSystem.Authentication.Api.Data;
using KlingerSystem.Authentication.Api.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Security.Jwt;
using NetDevPack.Security.Jwt.Store.EntityFrameworkCore;

namespace KlingerSystem.Authentication.Api.Configuration
{
    public static class IdentityConfiguration
    {
        public static void IdentityConfig(this IServiceCollection services, IConfiguration configuration)
        {

            var appSettingsSection = configuration.GetSection("AppTokenSettings");
            services.Configure<AppTokenSettings>(appSettingsSection);

            services.AddJwksManager()
                .PersistKeysToDatabaseStore<AuthenticationDbContext>();

            services.AddDbContext<AuthenticationDbContext>(option => option.UseSqlServer(configuration.GetConnectionString("Connection")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddErrorDescriber<IdentityMessagePtBr>()
                .AddEntityFrameworkStores<AuthenticationDbContext>()
                .AddDefaultTokenProviders();

            services.AddJwtConfiguration(configuration);
        }
    }
}
