using EBP.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EBP.Infrastructure.BackgroundJob.Services
{
    internal class ReleaseBookedTicketBackgroundService(
        IServiceScopeFactory _scopeFactory,
        IOptions<BackgroundJobsOptions> optionsAccessor,
        ILogger<ReleaseBookedTicketBackgroundService> logger)
        : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var allowedBookedPeriod = optionsAccessor.Value.AllowedExpirationBookedPeriod;

            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();

                try
                {
                    var dbSessionRepository = scope.ServiceProvider.GetRequiredService<IDbSessionRepository>();
                    var bookingTicketRepository = scope.ServiceProvider.GetRequiredService<IBookingTicketRepository>();

                    var expiredBookedTickets = await bookingTicketRepository.GetExpiredBookedTicketsAsync(allowedBookedPeriod, stoppingToken);

                    foreach (var expiredBookedTicket in expiredBookedTickets)
                        expiredBookedTicket.ReleaseBooking();

                    await dbSessionRepository.SaveChangesAsync(stoppingToken);
                }
                catch (Exception e)
                {
                    logger.LogError(e, "An error occurred while releasing booked event tickets. Error message: {ErrorMessage}", e.Message);
                }

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }
}
