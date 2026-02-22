namespace EBP.Domain.Repositories
{
    public interface IDbSessionRepository
    {
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<bool> SaveChangesAsync<TRespository>(Func<TRespository, Task> action, CancellationToken cancellationToken = default)
            where TRespository : ISessionRepository;
    }
}
