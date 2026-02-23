using MediatR;

namespace EBP.Application.Commands
{
    public record class RemoveEventTicketCommand(
        Guid EventId,
        Guid TicketId) : IRequest;
}