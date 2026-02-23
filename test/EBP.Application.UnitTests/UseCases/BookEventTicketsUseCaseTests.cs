using EBP.Application.Commands;
using EBP.Application.DTOs;
using EBP.Application.UseCases;
using EBP.Domain.Entities;
using EBP.Domain.Enums;
using EBP.Domain.Exceptions;
using EBP.Domain.Providers;
using EBP.Domain.Repositories;
using Moq;

namespace EBP.Application.UnitTests.UseCases
{
    public class BookEventTicketsUseCaseTests
    {
        [Test]
        public void GivenEventDoesNotExist_ShouldThrowEventNotFoundException()
        {
            var command = new BookEventTicketsCommand(Guid.NewGuid(), [new(TicketKindDto.Regular, 2)]);
            var (useCase, eventRepository, _) = CreateUseCase();
            eventRepository
                .Setup(_ => _.GetAsync(command.EventId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(default(Event));

            Assert.ThrowsAsync<EventNotFoundException>(() => useCase.Handle(command, CancellationToken.None));
        }

        [Test]
        public void GivenNotEnoughTicketsAvailable_ShouldThrowNotEnoughtTicketForBooking()
        {
            var @event = Event.CreateNew(
                "Test Event",
                "Description",
                DateTime.UtcNow.AddDays(1),
                TimeSpan.FromHours(2),
                [(TicketKind.Regular, 100m, 2)],
                DateTime.UtcNow);
            var command = new BookEventTicketsCommand(@event.Id, [new(TicketKindDto.Regular, 5)]);
            var (useCase, eventRepository, _) = CreateUseCase();
            eventRepository
                .Setup(_ => _.GetAsync(@event.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(@event);

            Assert.ThrowsAsync<NotEnoughtTicketForBooking>(() => useCase.Handle(command, CancellationToken.None));
        }

        [Test]
        public async Task GivenEventHasAvailableTickets_ShouldCreateBooking()
        {
            var @event = Event.CreateNew(
                "Test Event",
                "Description",
                DateTime.UtcNow.AddDays(1),
                TimeSpan.FromHours(2),
                [(TicketKind.Regular, 100m, 5)],
                DateTime.UtcNow);
            var command = new BookEventTicketsCommand(@event.Id, [new(TicketKindDto.Regular, 2)]);
            var (useCase, eventRepository, dbSessionRepository) = CreateUseCase();

            eventRepository
                .Setup(_ => _.GetAsync(@event.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(@event);

            dbSessionRepository
                .Setup(_ => _.SaveChangesAsync(It.IsAny<Func<IBookingRepository, Task>>(), It.IsAny<CancellationToken>()))
                .Callback<Func<IBookingRepository, Task>, CancellationToken>((action, ct) => action(new Mock<IBookingRepository>().Object))
                .ReturnsAsync(true);

            var result = await useCase.Handle(command, CancellationToken.None);

            Assert.That(result, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void GivenConcurrencyError_ShouldThrowTicketsBookingConcurrencyException()
        {
            var @event = Event.CreateNew(
                "Test Event",
                "Description",
                DateTime.UtcNow.AddDays(1),
                TimeSpan.FromHours(2),
                [(TicketKind.Regular, 100m, 5)],
                DateTime.UtcNow);
            var command = new BookEventTicketsCommand(@event.Id, [new(TicketKindDto.Regular, 1)]);
            var (useCase, eventRepository, dbSessionRepository) = CreateUseCase();

            eventRepository
                .Setup(_ => _.GetAsync(@event.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(@event);

            dbSessionRepository
                .Setup(_ => _.SaveChangesAsync(It.IsAny<Func<IBookingRepository, Task>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            Assert.ThrowsAsync<TicketsBookingConcurrencyException>(() => useCase.Handle(command, CancellationToken.None));
        }

        private static (BookEventTicketsUseCase useCase, Mock<IEventRepository> eventRepository, Mock<IDbSessionRepository> dbSessionRepository) CreateUseCase()
        {
            var dbSessionRepository = new Mock<IDbSessionRepository>();
            var eventRepository = new Mock<IEventRepository>();
            var applicationUserProvider = new Mock<IApplicationUserProvider>();
            applicationUserProvider
                .Setup(_ => _.Current)
                .Returns(ApplicationUser.Create(Guid.NewGuid().ToString(), "testuser"));
            var timeProvider = new Mock<ITimeProvider>();
            timeProvider
                .Setup(_ => _.Now)
                .Returns(DateTime.Now);

            var useCase = new BookEventTicketsUseCase(
                dbSessionRepository.Object,
                timeProvider.Object,
                applicationUserProvider.Object,
                eventRepository.Object);

            return (useCase, eventRepository, dbSessionRepository);
        }
    }
}
