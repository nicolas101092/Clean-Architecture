using CleanArchitecture.API.Infrastructure.Extensions.Dtos;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace CleanArchitecture.API.Infrastructure.Extensions
{
    /// <summary>
    /// Extension class for services to be added in ConfigureServices
    /// </summary>
    public static class ConfigureServiceExtension
    {
        private const string END_SERVICE_FILE = "Service";
        private const string END_VALIDATOR_FILE = "Validator";
        private const string END_REPOSITORY_FILE = "Repository";

        /// <summary>
        /// Defines swagger in Configure services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            SwaggerSettings swaggerConfig = new();
            configuration.GetSection("SwaggerConfig").Bind(swaggerConfig);

            services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc(swaggerConfig.Version, new OpenApiInfo
                    {
                        Version = swaggerConfig.Version,
                        Title = swaggerConfig.Title
                    });
                    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                }
            );

            return services;
        }

        /// <summary>
        /// Defines scrutor configuration
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddScrutor(this IServiceCollection services)
        {
            services.Scan(s => s.FromAssemblies(Assembly.Load(nameof(Application)), Assembly.Load(nameof(Infrastructure)))
                                .AddClasses(c => c.Where(e =>
                                {
                                    bool nameSpace = e.Namespace is not null && (e.Namespace.StartsWith($"{nameof(Application)}")
                                                                                    || e.Namespace.StartsWith($"{nameof(Infrastructure)}"));

                                    bool fileName = e.Name.EndsWith(END_SERVICE_FILE) || e.Name.EndsWith(END_VALIDATOR_FILE) || e.Name.EndsWith(END_REPOSITORY_FILE);

                                    return nameSpace && fileName;
                                }))
                                .AsImplementedInterfaces()
                                .WithScopedLifetime());

            return services;
        }
    }
}
