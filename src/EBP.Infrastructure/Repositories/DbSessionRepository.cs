using EBP.Domain.Repositories;

namespace EBP.Infrastructure.Repositories
{
    internal class DbSessionRepository(ApplicationDbContext applicationDbContext) : IDbSessionRepository
    {
        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
