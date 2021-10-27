using FluentValidation.Results;
using KlingerSystem.Api.Users;
using KlingerSystem.Core.Mediatr;
using KlingerSystem.Employee.Api.Application.Commands;
using KlingerSystem.Employee.Api.Application.Commands.Handler;
using KlingerSystem.Employee.Domain.Interfaces;
using KlingerSystem.Employee.Infrastructure.Data;
using KlingerSystem.Employee.Infrastructure.Repository;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace KlingerSystem.Employee.Api.Configuration
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
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<EmployeeContext>();

            //Commands
            services.AddScoped<IRequestHandler<RegisterTheFirstEmployeeCommand, ValidationResult>, EmployeeCommandHandler>();
            services.AddScoped<IRequestHandler<CommonEmployeeRegistrationCommand, ValidationResult>, EmployeeCommandHandler>();

            //Events
            

            //Querys

            
        }
    }
}
