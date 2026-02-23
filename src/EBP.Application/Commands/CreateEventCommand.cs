using MediatR;

namespace EBP.Application.Commands
{
    public record class CreateEventCommand(
        string? Name,
        string? Description,
        DateTime StartAt,
        TimeSpan Duration,
        int StandartTicketsCount,
        int VipTicketsCount,
        int StudentTicketsCount)
        : IRequest<Guid>
    {
    }
}
