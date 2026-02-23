using EBP.Application.DTOs;
using MediatR;

namespace EBP.Application.Commands
{
    public record class AddEventTicketCommand(
        Guid EventId,
        TicketKindDto Kind,
        decimal Price) : IRequest<Guid>;
}