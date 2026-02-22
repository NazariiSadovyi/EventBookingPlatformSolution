using EBP.Application.Commands;
using EBP.Domain.Entities;
using EBP.Domain.Exceptions;
using EBP.Domain.Providers;
using EBP.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EBP.Application.UseCases
{
    internal class BookTicketsUseCase(
        IBookingTicketRepository bookingTicketRepository,
        IDbSessionRepository dbSessionRepository,
        ITimeProvider timeProvider,
        IApplicationUserProvider applicationUserProvider,
        ILogger<BookTicketsUseCase> logger)
        : IRequestHandler<BookTicketsCommand, IEnumerable<BookingTicket>>
    {
        public async Task<IEnumerable<BookingTicket>> Handle(BookTicketsCommand request, CancellationToken cancellationToken)
        {
            var tickets = await bookingTicketRepository.GetTicketsAsync(request.BookingTicketIds, cancellationToken);
            
            foreach (var ticket in tickets)
                ticket.BookForPurchasing(timeProvider.Now, applicationUserProvider.Current);

            try
            {
                await dbSessionRepository.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                logger.LogError(ex, "Concurrency exception occurred while booking event tickets with ids: {BookingTicketIds}", request.BookingTicketIds);
                throw new BookingTicketsBookingConcurrencyException(request.BookingTicketIds);
            }

            return tickets;
        }
    }
}
