using MediatR;

namespace EBP.Application.Interfaces
{
    public interface IBackgroundRequestHandler
    {
        Task HandleAsync(CancellationToken cancellationToken = default);
    }
}
