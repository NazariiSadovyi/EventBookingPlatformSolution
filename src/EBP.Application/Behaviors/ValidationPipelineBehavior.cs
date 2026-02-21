using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace EBP.Application.Behaviors
{
    internal class ValidationPipelineBehavior<TRequest, TResponse>(IServiceProvider serviceProvider)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var validator = serviceProvider.GetService<IValidator<TRequest>>();
            if (validator is not null)
                await validator.ValidateAndThrowAsync(request, cancellationToken);

            return await next.Invoke(cancellationToken);
        }
    }
}
