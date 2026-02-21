namespace EBP.Domain.Repositories
{
    public interface IDbSessionRepository
    {
        public Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
