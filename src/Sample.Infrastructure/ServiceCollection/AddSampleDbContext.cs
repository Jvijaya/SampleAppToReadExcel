using Sample.Application.Interfaces;
using Sample.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sample.Application.Extensions;

namespace Sample.Infrastructure.ServiceCollection
{
    public static class CustomServiceCollectionExtenstion
    {
        public static IServiceCollection AddSampleDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddConfig<SampleDatabaseConfig>(configuration);
            var dbConfig = configuration.LoadConfig<SampleDatabaseConfig>();
            services.AddDbContext<SampleDbContext>(options =>
            {
                options.UseSqlServer(dbConfig.ConnectionString,
                    providerOptions => providerOptions.EnableRetryOnFailure());
            });

            services.AddScoped<ISampleDbContext>(provider => provider.GetService<SampleDbContext>());
            return services;
        }
    }
}
