using EBP.Application.Commands;
using EBP.Domain.Entities;
using EBP.Domain.Exceptions;
using EBP.Domain.Providers;
using EBP.Domain.Repositories;
using MediatR;

namespace EBP.Application.UseCases
{
    internal class BookTicketsUseCase(
        IDbSessionRepository dbSessionRepository,
        ITimeProvider timeProvider,
        IApplicationUserProvider applicationUserProvider)
        : IRequestHandler<BookTicketsCommand, IEnumerable<BookingTicket>>
    {
        public async Task<IEnumerable<BookingTicket>> Handle(BookTicketsCommand request, CancellationToken cancellationToken)
        {
            IEnumerable<BookingTicket>? tickets = null;

            var result = await dbSessionRepository.SaveChangesAsync<IBookingTicketRepository>(async bookingTicketRepository =>
            {
                tickets = await bookingTicketRepository.GetTicketsAsync(request.BookingTicketIds, cancellationToken);
                foreach (var ticket in tickets)
                    ticket.BookForPurchasing(timeProvider.Now, applicationUserProvider.Current);
            }, cancellationToken);

            if (!result)
                throw new BookingTicketsBookingConcurrencyException(request.BookingTicketIds);

            return tickets!;
        }
    }
}
