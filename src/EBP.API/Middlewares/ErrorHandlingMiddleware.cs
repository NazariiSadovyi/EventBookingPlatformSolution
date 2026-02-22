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

                switch (exception)
                {
                    case ValidationException validationException:
                        details.Type = "ValidationException";
                        details.Message = string.Join(", ", validationException.Errors.Select(_ => $"Property: {_.PropertyName}, Error: {_.ErrorMessage}"));
                        details.Code = StatusCodes.Status400BadRequest;
                        logger.LogWarning(validationException, "Somebody sent invalid request, oops");  
                        break;
                    case DomainExceptionBase domainException:
                        details.Type = "DomainException";
                        details.Message = domainException.Message;
                        details.Code = StatusCodes.Status400BadRequest;
                        logger.LogWarning(domainException, "Domain exception occured");
                        break;
                    default:
                        details.Type = "UnhandledException";
                        details.Message = "An unhandled exception has occured, please contact support.";
                        details.Code = StatusCodes.Status500InternalServerError;
                        logger.LogError(exception, "Unhandled exception occured");
                        break;
                }

                httpContext.Response.StatusCode = StatusCodes.Status200OK;
                await httpContext.Response.WriteAsJsonAsync(details);
            }
        }
    }
}
