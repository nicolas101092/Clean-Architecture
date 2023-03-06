using Microsoft.ApplicationInsights.Extensibility;
using Serilog;

namespace CleanArchitecture.API.Infrastructure.Extensions
{
    /// <summary>
    /// Class with serilogConfiguration
    /// </summary>
    public static class SerilogExtension
    {
        private const string LOGGER_NAME = "log/log.txt";
        private const string MESSAGE = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{CorrelationId}] [{Level:u3}] {Message:lj}{NewLine}{Exception}";
        private const string CORRELATION_ID = "CorrelationId";

        /// <summary>
        /// Method that configures Serilog
        /// </summary>
        /// <param name="builder">Web application builder</param>
        public static void AddConfiguration(WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog();

            var config = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console(outputTemplate: MESSAGE)
                .WriteTo.File(LOGGER_NAME, outputTemplate: MESSAGE, rollingInterval: RollingInterval.Day)
                .Enrich.FromLogContext()
                .Enrich.WithCorrelationId()
                .Enrich.WithCorrelationIdHeader(CORRELATION_ID);

            try
            {
                Log.Logger = config
                .WriteTo.ApplicationInsights(new TelemetryConfiguration
                {
                    ConnectionString = builder.Configuration.GetSection("ApplicationInsights:ConnectionString").Value
                }, TelemetryConverter.Traces)
                .CreateLogger();
            }
            catch (ArgumentException)
            {
                Log.Logger = config.CreateLogger();
                Log.Logger.Information("Logger creado sin conexión a ApplicationInsights");
            }
        }
    }
}
