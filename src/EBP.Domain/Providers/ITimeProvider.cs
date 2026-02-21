namespace EBP.Domain.Providers
{
    public interface ITimeProvider
    {
        DateTime Now { get; }
    }
}
