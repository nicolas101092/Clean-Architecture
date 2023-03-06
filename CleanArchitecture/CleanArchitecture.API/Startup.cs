using Application.Extensions;
using Application.Mapper;
using CleanArchitecture.API.Infrastructure.Extensions;
using CleanArchitecture.API.Infrastructure.Startup;
using System.Reflection;

namespace CleanArchitecture.API
{
    /// <summary>
    /// Configures the application's services and request routing.
    /// </summary>
    public static class Startup
    {
        /// <summary>
        /// Configures the reusable components that provide application functionality
        /// </summary>
        /// <param name="builder">Web application builder</param>
        /// <returns>Web application builder</returns>
        public static WebApplicationBuilder ConfigureService(this WebApplicationBuilder builder)
        {
            builder.Services.AddScrutor();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwagger(builder.Configuration);
            builder.Services.AddApplicationLayer(builder.Configuration);
            builder.Services.AddAutoMapper(typeof(BaseAutoMapper).GetTypeInfo().Assembly);
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddApplicationInsightsTelemetry();

            return builder;
        }

        /// <summary>
        /// Create the request processing pipeline of the application
        /// </summary>
        /// <param name="builder">Web application builder</param>
        public static void Configure(this WebApplicationBuilder builder)
        {
            builder.Build().Configure();
        }
    }
}
