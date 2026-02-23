using EBP.Application.Commands;
using EBP.Application.Converters;
using EBP.Domain.Entities;
using EBP.Domain.Exceptions;
using EBP.Domain.Providers;
using EBP.Domain.Repositories;
using MediatR;

namespace EBP.Application.UseCases
{
    public class BookEventTicketsUseCase(
        IDbSessionRepository _dbSessionRepository,
        ITimeProvider _timeProvider,
        IApplicationUserProvider _applicationUserProvider,
        IEventRepository _eventRepository)
        : IRequestHandler<BookEventTicketsCommand, Guid>
    {
        public async Task<Guid> Handle(BookEventTicketsCommand command, CancellationToken cancellationToken)
        {
            var @event = await _eventRepository.GetAsync(command.EventId, cancellationToken);
            if (@event is null)
                throw new EventNotFoundException(command.EventId);

            var availableTikets = @event.Tickets.Where(t => t.IsAvailable).ToArray();
            var ticketsToBook = new List<Ticket>();

            foreach (var ticketDetail in command.TicketDetails)
            {
                var ticketKind = ticketDetail.Kind.ToDomain();
                var tickets = availableTikets.Where(t => t.Type.Kind == ticketKind).Take(ticketDetail.TicketCount).ToArray();
                if (tickets.Length != ticketDetail.TicketCount)
                    throw new NotEnoughtTicketForBooking(ticketKind);

                ticketsToBook.AddRange(tickets);
            }

            Booking booking = null!;

            var result = await _dbSessionRepository.SaveChangesAsync<IBookingRepository>(async bookingRepository =>
            {
                booking = Booking.CreateNew(@event, ticketsToBook, _applicationUserProvider.Current.UserId, _timeProvider.Now);
                await bookingRepository.AddAsync(booking, cancellationToken);
            }, cancellationToken);

            if (!result)
                throw new TicketsBookingConcurrencyException();

            return booking.Id;
        }
    }
}
