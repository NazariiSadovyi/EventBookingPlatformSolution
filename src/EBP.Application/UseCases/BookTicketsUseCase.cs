using EBP.Application.Commands;
using EBP.Domain.Entities;
using EBP.Domain.Enums;
using EBP.Domain.Exceptions;
using EBP.Domain.Providers;
using EBP.Domain.Repositories;
using MediatR;

namespace EBP.Application.UseCases
{
    internal class BookEventTicketsUseCase(
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
            var standartTickets = availableTikets.Where(t => t.Type == TicketType.Standard).Take(command.StandartTicketCount).ToArray();
            var vipTickets = availableTikets.Where(t => t.Type == TicketType.VIP).Take(command.VipTicketCount).ToArray();
            var studentTickets = availableTikets.Where(t => t.Type == TicketType.Student).Take(command.StudentTicketCount).ToArray();

            if (standartTickets.Length != command.StandartTicketCount
                || vipTickets.Length != command.VipTicketCount
                || studentTickets.Length != command.StudentTicketCount)
                throw new NotEnoughtTicketForBooking();

            Booking booking = null!;

            var result = await _dbSessionRepository.SaveChangesAsync<IBookingRepository>(async bookingRepository =>
            {
                var tickets = standartTickets.Concat(vipTickets).Concat(studentTickets);
                booking = Booking.CreateNew(@event, tickets, _applicationUserProvider.Current.UserId, _timeProvider.Now);
                await bookingRepository.AddAsync(booking, cancellationToken);
            }, cancellationToken);

            if (!result)
                throw new TicketsBookingConcurrencyException();

            return booking.Id;
        }
    }
}
