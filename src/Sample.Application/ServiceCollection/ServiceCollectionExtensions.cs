using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sample.Application.Interfaces.Services;
using Sample.Application.Services;
using Sample.Domain.Models;
using System.Reflection;

namespace Sample.Application.ServiceCollection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddAutoMapper(typeof(EmployeeModel))
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
