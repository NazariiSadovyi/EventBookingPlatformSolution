using MediatR;

namespace EBP.Application.Commands
{
    public record class BookEventTicketsCommand(
        Guid EventId,
        int StandartTicketCount,
        int VipTicketCount,
        int StudentTicketCount)
        : IRequest<Guid>
    {
    }
}
