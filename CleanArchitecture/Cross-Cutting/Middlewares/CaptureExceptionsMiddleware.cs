using Cross_Cutting.Extensions.ExceptionExtensions;
using Cross_Cutting.Middlewares.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Cross_Cutting.Middlewares
{
    /// <summary>
    /// CaptureExceptionsMiddleware class.
    /// </summary>
    public class CaptureExceptionsMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// CaptureExceptionsMiddleware's Constructor.
        /// </summary>
        /// <param name="next">RequestDeletegate object type</param>
        public CaptureExceptionsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Process a generated exception and generate a log asociated.
        /// </summary>
        /// <param name="httpContext">HttpContext object type</param>
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (NotFoundException exception)
            {
                await ProcessException(httpContext: httpContext,
                                       titleError: ConstantsExceptions.TITTLE_NOTFOUND_EXCEPTION,
                                       detail: exception.Message,
                                       statusCode: StatusCodes.Status404NotFound,
                                       trace: exception.StackTrace);
            }
            catch (BadRequestException exception)
            {
                await ProcessValidationException(httpContext: httpContext,
                                                 titleError: ConstantsExceptions.TITTLE_BADREQUEST_EXCEPTION,
                                                 exception: exception);
            }
            catch (ConflictedException exception)
            {
                await ProcessException(httpContext: httpContext,
                                      titleError: ConstantsExceptions.TITTLE_CONFLICT_EXCEPTION,
                                      detail: exception.Message,
                                      statusCode: StatusCodes.Status409Conflict,
                                      trace: exception.StackTrace);
            }
            catch (Exception exception)
            {
                await ProcessException(httpContext: httpContext,
                                       titleError: ConstantsExceptions.TITTLE_INTERNALSERVERERRROR_EXCEPTION,
                                       detail: exception.Message,
                                       statusCode: StatusCodes.Status500InternalServerError,
                                       trace: exception.StackTrace);

            }
        }

        #region "Private methods"

        /// <summary>
        /// Init response.
        /// </summary>
        /// <param name="httpContext">HttpContext object type</param>
        /// <param name="statusCode">Int object type</param>
        /// <returns>HttpContext object type</returns>
        private static HttpContext InitResponse(HttpContext httpContext, int statusCode)
        {
            httpContext.Response.Clear();
            httpContext.Response.StatusCode = statusCode;
            httpContext.Response.Headers.Add("Content-Type", "application/json");

            return httpContext;
        }

        /// <summary>
        /// Process validation exception
        /// </summary>
        /// <param name="httpContext">HttpContext object type</param>
        /// <param name="titleError">Title of error</param>
        /// <param name="exception">Exception</param>
        /// <returns></returns>
        private async Task ProcessValidationException(HttpContext httpContext, string titleError, BadRequestException exception)
        {
            httpContext = InitResponse(httpContext, StatusCodes.Status400BadRequest);

            ValidationProblemDetails problem = CreateValidationProblemDetails(httpContext,
                                                                                new DtoDetailValidationException
                                                                                {
                                                                                    Title = titleError,
                                                                                    Status = StatusCodes.Status400BadRequest,
                                                                                    Detail = exception.Message,
                                                                                    Trace = exception.StackTrace ?? string.Empty,
                                                                                    Errors = exception.Errors
                                                                                });

            string result = JsonConvert.SerializeObject(problem);

            await httpContext.Response.WriteAsync(result);
        }

        /// <summary>
        /// Create validation problem details
        /// </summary>
        /// <param name="httpContext">HttpContext object type</param>
        /// <param name="detailException"></param>
        /// <returns></returns>
        private ValidationProblemDetails CreateValidationProblemDetails(HttpContext httpContext, DtoDetailValidationException detailException)
        {
            ValidationProblemDetails result = detailException.Errors != null ? new ValidationProblemDetails(detailException.Errors) : new ValidationProblemDetails();
            result.Type = $"https://httpstatuses.com/{detailException.Status}";
            result.Title = detailException.Title;
            result.Status = detailException.Status;
            result.Detail = detailException.Detail;
            result.Instance = httpContext.Request.Path;

            result.Extensions["trace"] = detailException.Trace;

            return result;
        }

        /// <summary>
        /// Process Log exception.
        /// </summary>
        /// <param name="httpContext">HttpContext object type</param>
        /// <param name="detail">String object type</param>
        /// <param name="detail">String object type</param>
        /// <param name="statusCode">Int object type</param>
        private async Task ProcessException(HttpContext httpContext, string titleError, string detail, int statusCode, string? trace)
        {
            httpContext = InitResponse(httpContext, statusCode);

            ProblemDetails dtoBaseLog = CreateProblemDetails(context: httpContext,
                                                                        new DtoDetailException
                                                                        {
                                                                            Title = titleError,
                                                                            Status = statusCode,
                                                                            Detail = detail,
                                                                            Trace = trace ?? string.Empty
                                                                        });

            string result = JsonConvert.SerializeObject(dtoBaseLog);

            await httpContext.Response.WriteAsync(result);
        }

        /// <summary>
        /// Adapt dto information to fill a ProblemData object 
        /// </summary>
        /// <param name="context">HttpContext object type</param>
        /// <param name="detailException">Dto with dat</param>
        /// <returns>A ProblemDetail object filled with the specific problem</returns>
        private ProblemDetails CreateProblemDetails(HttpContext context, DtoDetailException detailException)
        {
            ProblemDetails result = new()
            {
                Type = $"https://httpstatuses.com/{detailException.Status}",
                Title = detailException.Title,
                Status = detailException.Status,
                Detail = detailException.Detail,
                Instance = context.Request.Path
            };

            result.Extensions["trace"] = detailException.Trace;

            return result;
        }

        #endregion
    }
}
