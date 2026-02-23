using EBP.API.Models;
using EBP.Domain.Exceptions;
using FluentValidation;

namespace EBP.API.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext httpContext,
            ILogger<ErrorHandlingMiddleware> logger)
        {
            try
            {
                await _next.Invoke(httpContext);
            }
            catch (Exception exception)
            {
                var details = new ErrorDetailsResponse();
                var code = StatusCodes.Status500InternalServerError;

                switch (exception)
                {
                    case ValidationException validationException:
                        details.Type = "ValidationException";
                        details.Message = string.Join(", ", validationException.Errors.Select(_ => $"Property: {_.PropertyName}, Error: {_.ErrorMessage}"));
                        code = StatusCodes.Status400BadRequest;
                        logger.LogWarning(validationException, "Somebody sent invalid request, oops");  
                        break;
                    case DomainExceptionBase domainException:
                        details.Type = "DomainException";
                        details.Message = domainException.Message;
                        code = StatusCodes.Status400BadRequest;
                        logger.LogWarning(domainException, "Domain exception occured");
                        break;
                    default:
                        details.Type = "UnhandledException";
                        details.Message = "An unhandled exception has occured, please contact support.";
                        logger.LogError(exception, "Unhandled exception occured");
                        break;
                }

                httpContext.Response.StatusCode = code;
                await httpContext.Response.WriteAsJsonAsync(details);
            }
        }
    }
}
