using CleanArchitecture.API.Infrastructure.Extensions;

namespace CleanArchitecture.API.Infrastructure.Startup
{
    /// <summary>
    /// Configure Startup's class
    /// </summary>
    public static class ConfigureStartup
    {
        /// <summary>
        /// Create the request processing pipeline of the application
        /// </summary>
        /// <param name="app">Web application</param>
        public static void Configure(this WebApplication app)
        {
            app.UseCaptureExceptionsMiddleware();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
