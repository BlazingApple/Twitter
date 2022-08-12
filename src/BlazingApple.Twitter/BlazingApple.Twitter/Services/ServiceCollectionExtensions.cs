using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlazingApple.Twitter.Services
{
    /// <summary>Extensions for Twitter.</summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>Add services for getting and sending tweets, including error logs</summary>
        /// <param name="services">Collection where the service should be registered</param>
        /// <param name="configRoot">Configuration containing the "Twitter" section</param>
        /// <returns><paramref name="services" /> (fluent API)</returns>
        public static IServiceCollection AddTwitter(this IServiceCollection services, IConfiguration configRoot)
        {
            IConfigurationSection config = configRoot.GetSection("Twitter");
            services.Configure<TwitterSettings>(config);
            services.AddSingleton<TwitterService>();

            return services;
        }
    }
}
