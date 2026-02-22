using EBP.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EBP.Infrastructure.Repositories
{
    internal class DbSessionRepository(
        ApplicationDbContext _applicationDbContext,
        IServiceProvider _serviceProvider,
        ILogger<DbSessionRepository> _logger)
        : IDbSessionRepository
    {
        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _applicationDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> SaveChangesAsync<TRespository>(Func<TRespository, Task> action, CancellationToken cancellationToken = default)
            where TRespository : ISessionRepository
        {
            try
            {
                await action(_serviceProvider.GetRequiredService<TRespository>());
                await _applicationDbContext.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                _logger.LogWarning("Concurrency conflict detected while saving changes. The operation will be retried.");
                return false;
            }
        }
    }
}
