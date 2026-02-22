using EBP.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EBP.Infrastructure.Services
{
    internal class EngineBackgroundService<TBackgroundRequestHandler>(
        IServiceScopeFactory _scopeFactory,
        ILogger<EngineBackgroundService<TBackgroundRequestHandler>> _logger)
        : BackgroundService
        where TBackgroundRequestHandler : IBackgroundRequestHandler
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();

                try
                {
                    var requestHandler = scope.ServiceProvider.GetRequiredService<TBackgroundRequestHandler>();
                    await requestHandler.HandleAsync(stoppingToken);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "An error occurred while releasing booked event tickets. Error message: {ErrorMessage}", e.Message);
                }

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }
}
