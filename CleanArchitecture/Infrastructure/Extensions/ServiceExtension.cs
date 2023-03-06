using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    /// <summary>
    /// Static class defining the dependency injection at the Infrastructure layer
    /// </summary>
    public static class ServiceExtension
    {
        /// <summary>
        /// Static method defining the dependency injection at the Infrastructure layer
        /// </summary>
        /// <param name="services">ServicesCollection interface</param>
        /// <param name="configuration">Configuration interface</param>
        public static void AddInfraestructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DummyContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(DummyContext).Assembly.FullName)));
        }
    }
}
