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
                logger.LogError(
                    exception,
                    "Error has happened with {RequestPath}, the message is {ErrorMessage}",
                    httpContext.Request.Path.Value, exception.Message);

                object details;
                var statusCode = StatusCodes.Status500InternalServerError;

                switch (exception)
                {
                    case ValidationException validationException:
                        details = validationException.Errors.Select(_ => $"Property: {_.PropertyName}, Error: {_.ErrorMessage}").ToArray();
                        statusCode = StatusCodes.Status400BadRequest;
                        logger.LogInformation(validationException, "Somebody sent invalid request, oops");  
                        break;
                    case DomainExceptionBase domainException:
                        details = domainException.Message;
                        statusCode = StatusCodes.Status400BadRequest;
                        logger.LogError(domainException, "Domain exception occured");
                        break;
                    default:
                        details = exception.Message;
                        statusCode = StatusCodes.Status500InternalServerError;
                        logger.LogError(exception, "Unhandled exception occured");
                        break;
                }

                httpContext.Response.StatusCode = statusCode;
                await httpContext.Response.WriteAsJsonAsync(details);
            }
        }
    }
}
