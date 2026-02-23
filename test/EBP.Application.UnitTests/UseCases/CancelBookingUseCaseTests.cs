using EBP.Application.Commands;
using EBP.Application.UseCases;
using EBP.Domain.Entities;
using EBP.Domain.Enums;
using EBP.Domain.Exceptions;
using EBP.Domain.Providers;
using EBP.Domain.Repositories;
using Moq;

namespace EBP.Application.UnitTests.UseCases
{
    public class CancelBookingUseCaseTests
    {
        [Test]
        public void GivenBookingDoesNotExist_ShouldThrowBookingNotFoundException()
        {
            var command = new CancelBookingCommand(Guid.NewGuid());
            var (useCase, bookingRepository, _, _) = CreateUseCase();
            bookingRepository
                .Setup(_ => _.GetAsync(command.BookingId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(default(Booking));

            Assert.ThrowsAsync<BookingNotFoundException>(() => useCase.Handle(command, CancellationToken.None));
        }

        [Test]
        public async Task GivenBookingExistsAndEventIsMoreThanOneDayAway_ShouldCancelAndCreateRefund()
        {
            var @event = Event.CreateNew(
                "Test Event",
                "Description",
                DateTime.UtcNow.AddDays(3),
                TimeSpan.FromHours(2),
                [(TicketKind.Regular, 100m, 5)],
                DateTime.UtcNow);
            var tickets = @event.Tickets.Take(2).ToList();
            var booking = Booking.CreateNew(@event, tickets, string.Empty, DateTime.UtcNow);
            booking.SubmitBooking();
            var command = new CancelBookingCommand(booking.Id);
            var (useCase, bookingRepository, bookingRefundRepository, timeProvider) = CreateUseCase();

            bookingRepository
                .Setup(_ => _.GetAsync(booking.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(booking);
            timeProvider
                .Setup(_ => _.Now)
                .Returns(DateTime.UtcNow);

            await useCase.Handle(command, CancellationToken.None);

            bookingRefundRepository.Verify(
                _ => _.AddAsync(It.Is<BookingRefund>(_ => _.Booking.Id == booking.Id), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Test]
        public async Task GivenBookingExistsAndEventIsWithinOneDayAway_ShouldCancelWithoutRefund()
        {
            var eventStartTime = DateTime.UtcNow.AddHours(12);
            var @event = Event.CreateNew(
                "Test Event",
                "Description",
                eventStartTime,
                TimeSpan.FromHours(2),
                [(TicketKind.Regular, 100m, 5)],
                DateTime.UtcNow);
            var tickets = @event.Tickets.Take(2).ToList();
            var booking = Booking.CreateNew(@event, tickets, string.Empty, DateTime.UtcNow);
            booking.SubmitBooking();
            var command = new CancelBookingCommand(booking.Id);
            var (useCase, bookingRepository, bookingRefundRepository, timeProvider) = CreateUseCase();

            bookingRepository
                .Setup(_ => _.GetAsync(booking.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(booking);
            timeProvider
                .Setup(_ => _.Now)
                .Returns(DateTime.UtcNow);

            await useCase.Handle(command, CancellationToken.None);

            bookingRefundRepository.Verify(
                _ => _.AddAsync(It.IsAny<BookingRefund>(), It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Test]
        public async Task GivenValidBooking_ShouldUpdateBookingStatus()
        {
            var @event = Event.CreateNew(
                "Test Event",
                "Description",
                DateTime.UtcNow.AddDays(5),
                TimeSpan.FromHours(2),
                [(TicketKind.Regular, 100m, 5)],
                DateTime.UtcNow);
            var tickets = @event.Tickets.Take(2).ToList();
            var booking = Booking.CreateNew(@event, tickets, string.Empty, DateTime.UtcNow);
            booking.SubmitBooking();
            var command = new CancelBookingCommand(booking.Id);
            var (useCase, bookingRepository, _, timeProvider) = CreateUseCase();

            bookingRepository
                .Setup(_ => _.GetAsync(booking.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(booking);
            timeProvider
                .Setup(_ => _.Now)
                .Returns(DateTime.UtcNow);

            await useCase.Handle(command, CancellationToken.None);

            Assert.That(booking.Status, Is.EqualTo(BookingStatus.Cancelled));
        }

        private static (CancelBookingUseCase useCase, Mock<IBookingRepository> bookingRepository, Mock<IBookingRefundRepository> bookingRefundRepository, Mock<ITimeProvider> timeProvider) CreateUseCase()
        {
            var dbSessionRepository = new Mock<IDbSessionRepository>();
            dbSessionRepository
                .Setup(_ => _.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            var bookingRepository = new Mock<IBookingRepository>();
            var bookingRefundRepository = new Mock<IBookingRefundRepository>();
            var timeProvider = new Mock<ITimeProvider>();

            var useCase = new CancelBookingUseCase(
                dbSessionRepository.Object,
                bookingRepository.Object,
                bookingRefundRepository.Object,
                timeProvider.Object);

            return (useCase, bookingRepository, bookingRefundRepository, timeProvider);
        }
    }
}
