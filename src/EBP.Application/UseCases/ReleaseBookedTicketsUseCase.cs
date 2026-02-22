using EBP.Application.Interfaces;
using EBP.Domain.Repositories;

namespace EBP.Application.UseCases
{
    public class ReleaseBookedTicketsUseCase(
        IBookingTicketRepository _bookingTicketRepository,
        IDbSessionRepository _dbSessionRepository)
        : IBackgroundRequestHandler
    {
        public async Task HandleAsync(CancellationToken cancellationToken = default)
        {
            var expiredBookedTickets = await _bookingTicketRepository.GetExpiredBookedTicketsAsync(cancellationToken);

            foreach (var expiredBookedTicket in expiredBookedTickets)
                expiredBookedTicket.ReleaseBooking();

            await _dbSessionRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
