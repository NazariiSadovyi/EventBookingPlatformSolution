using EBP.Infrastructure.BackgroundJob.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EBP.Infrastructure.BackgroundJob
{
    public static class InfrastructureBackgroundJobServiceCollectionExtensions
    {
        public static void AddInfrastructureBackgroundJob(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<BackgroundJobsOptions>(configuration.GetSection(BackgroundJobsOptions.BackgroundJobs));
            services.AddHostedService<ReleaseBookedTicketBackgroundService>();
        }
    }
}
