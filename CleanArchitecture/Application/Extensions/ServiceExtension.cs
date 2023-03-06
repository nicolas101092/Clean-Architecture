using Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    /// <summary>
    /// Static class defining the dependency injection at the application layer
    /// </summary>
    public static class ServiceExtension
    {
        /// <summary>
        /// Static method defining the dependency injection at the application layer
        /// </summary>
        /// <param name="services">ServicesCollection interface</param>
        /// <param name="configuration">Configuration interface</param>
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfraestructureLayer(configuration);
        }
    }
}
