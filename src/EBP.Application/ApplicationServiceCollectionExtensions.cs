using EBP.Application.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace EBP.Application
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped(_ => TimeProvider.System);

            services
                .AddMediatR(cfg => cfg
                    //.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>)) TODO: add validation pipeline behavior
                    .RegisterServicesFromAssemblyContaining<CreateBookingEventCommand>());
        }
    }
}
