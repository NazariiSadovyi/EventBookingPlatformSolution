using EBP.Application.Commands;
using EBP.Application.UseCases;
using EBP.Domain.Entities;
using EBP.Domain.Enums;
using EBP.Domain.Exceptions;
using EBP.Domain.Repositories;
using Moq;

namespace EBP.Application.UnitTests.UseCases
{
    public class SubmitBookingUseCaseTests
    {
        [Test]
        public void GivenBookingDoesNotExist_ShouldThrowBookingNotFoundException()
        {
            var command = new SubmitBookingCommand(Guid.NewGuid());
            var (useCase, bookingRepository) = CreateUseCase();
            bookingRepository
                .Setup(_ => _.GetAsync(command.BookingId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(default(Booking));

            Assert.ThrowsAsync<BookingNotFoundException>(() => useCase.Handle(command, CancellationToken.None));
        }

        [Test]
        public async Task GivenValidBooking_ShouldChangeBookingState()
        {
            var @event = Event.CreateNew(
                "Test Event",
                "Description",
                DateTime.UtcNow.AddDays(1),
                TimeSpan.FromHours(2),
                [(TicketKind.Regular, 100m, 5), (TicketKind.VIP, 200m, 3)],
                DateTime.UtcNow);
            var tickets = @event.Tickets.Take(3).ToList();
            var booking = Booking.CreateNew(@event, tickets, string.Empty, DateTime.UtcNow);
            var command = new SubmitBookingCommand(booking.Id);
            var (useCase, bookingRepository) = CreateUseCase();

            bookingRepository
                .Setup(_ => _.GetAsync(booking.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(booking);

            await useCase.Handle(command, CancellationToken.None);

            Assert.That(booking.Status, Is.EqualTo(BookingStatus.Submitted));
        }

        private static (SubmitBookingUseCase useCase, Mock<IBookingRepository> bookingRepository) CreateUseCase()
        {
            var dbSessionRepository = new Mock<IDbSessionRepository>();
            dbSessionRepository
                .Setup(_ => _.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            var bookingRepository = new Mock<IBookingRepository>();

            var useCase = new SubmitBookingUseCase(
                dbSessionRepository.Object,
                bookingRepository.Object);

            return (useCase, bookingRepository);
        }
    }
}
