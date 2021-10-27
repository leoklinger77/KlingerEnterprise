using KlingerSystem.Bus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KlingerSystem.Employee.Api.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMessageBus(configuration?.GetSection("MessageQueueConnection")?["MessageBus"]);
        }
    }
}
