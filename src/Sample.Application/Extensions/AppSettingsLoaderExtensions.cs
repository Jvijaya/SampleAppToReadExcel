using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Sample.Application.Extensions
{
    public static class AppSettingsLoaderExtensions
    {
        public static T LoadConfig<T>(this IConfiguration configuration)
        {
            return configuration
                .LoadConfigSection<T>()
                .Get<T>();
        }

        public static IConfigurationSection LoadConfigSection<T>(this IConfiguration configuration)
        {
            var configSectionName = typeof(T).Name;
            return configuration.GetSection(configSectionName);
        }

        public static IServiceCollection AddConfig<T>(this IServiceCollection services, IConfiguration configuration) where T : class
        {
            var config = configuration.LoadConfig<T>();
            services.AddSingleton(config);
            return services;
        }
    }
}
