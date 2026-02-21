using EBP.Domain.Providers;

namespace EBP.Infrastructure.Providers
{
    internal class TimeProvider : ITimeProvider
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
