using EBP.Application.Behaviors;
using EBP.Application.Commands;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EBP.Application
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped(_ => TimeProvider.System);

            services.AddMediatR(_ => _
                .AddOpenBehavior(typeof(ValidationPipelineBehavior<,>))
                .RegisterServicesFromAssemblyContaining<CreateBookingEventCommand>());

            services.AddValidatorsFromAssemblyContaining<CreateBookingEventCommand>();
        }
    }
}
