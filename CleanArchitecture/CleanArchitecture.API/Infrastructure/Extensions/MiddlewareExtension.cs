using Cross_Cutting.Middlewares;

namespace CleanArchitecture.API.Infrastructure.Extensions
{
    /// <summary>
    /// Class to extend middleware exceptions
    /// </summary>
    public static class MiddlewareExtension
    {
        /// <summary>
        /// Method extension used to add the middleware to the HTTP request pipeline.
        /// </summary>
        public static IApplicationBuilder UseCaptureExceptionsMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CaptureExceptionsMiddleware>();
        }
    }
}
