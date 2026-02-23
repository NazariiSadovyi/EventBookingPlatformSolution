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
    public class CreateEventUseCaseTests
    {
        [Test]
        public async Task GivenValidEventDetails_ShouldCreateNewEvent()
        {
            var (useCase, eventRepository) = CreateUseCase();
            var command = new CreateEventCommand(
                "New Event",
                "Event Description",
                DateTime.UtcNow.AddDays(1),
                TimeSpan.FromHours(2),
                [new(TicketKind.Regular, 100m, 5), new(TicketKind.VIP, 200m, 3)]);

            eventRepository
                .Setup(_ => _.AddAsync(It.IsAny<Event>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(EventCreationResult.Success);

            var result = await useCase.Handle(command, CancellationToken.None);

            Assert.That(result, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void GivenEventNameAlreadyExists_ShouldThrowEventNameAlreadyExistsException()
        {
            var (useCase, eventRepository) = CreateUseCase();
            var command = new CreateEventCommand(
                "Existing Event",
                "Event Description",
                DateTime.UtcNow.AddDays(1),
                TimeSpan.FromHours(2),
                [new(TicketKind.Regular, 100m, 5)]);

            eventRepository
                .Setup(_ => _.AddAsync(It.IsAny<Event>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(EventCreationResult.NameAlreadyExists);

            Assert.ThrowsAsync<EventNameAlreadyExistsException>(() => useCase.Handle(command, CancellationToken.None));
        }

        [Test]
        public async Task GivenValidTicketDetails_ShouldCreateEventWithCorrectTickets()
        {
            var (useCase, eventRepository) = CreateUseCase();
            var command = new CreateEventCommand(
                "Concert Event",
                "A great concert",
                DateTime.UtcNow.AddDays(7),
                TimeSpan.FromHours(3),
                [new(TicketKind.Regular, 100m, 10), new(TicketKind.VIP, 250m, 5)]);

            eventRepository
                .Setup(_ => _.AddAsync(It.IsAny<Event>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(EventCreationResult.Success);

            var result = await useCase.Handle(command, CancellationToken.None);

            eventRepository.Verify(
                _ => _.AddAsync(It.Is<Event>(e => e.Tickets.Count == 15), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Test]
        public async Task GivenEventWithDescription_ShouldCreateEventWithAllDetails()
        {
            var (useCase, eventRepository) = CreateUseCase();
            var eventName = "Marathon 2024";
            var description = "Annual city marathon";
            var startTime = DateTime.UtcNow.AddDays(14);
            var duration = TimeSpan.FromHours(4);

            var command = new CreateEventCommand(
                eventName,
                description,
                startTime,
                duration,
                [new(TicketKind.Regular, 50m, 100)]);

            eventRepository
                .Setup(_ => _.AddAsync(It.IsAny<Event>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(EventCreationResult.Success);

            await useCase.Handle(command, CancellationToken.None);

            eventRepository.Verify(
                _ => _.AddAsync(
                    It.Is<Event>(e => e.Name == eventName && e.Desciption == description && e.StartAt == startTime && e.Duration == duration),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Test]
        public async Task GivenMultipleTicketKinds_ShouldCreateEventWithMixedTicketTypes()
        {
            var (useCase, eventRepository) = CreateUseCase();
            var command = new CreateEventCommand(
                "Premium Conference",
                "Annual industry conference",
                DateTime.UtcNow.AddDays(30),
                TimeSpan.FromHours(8),
                [new(TicketKind.Regular, 100m, 10), new(TicketKind.VIP, 250m, 5), new(TicketKind.Student, 50m, 5)]);

            eventRepository
                .Setup(_ => _.AddAsync(It.IsAny<Event>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(EventCreationResult.Success);

            await useCase.Handle(command, CancellationToken.None);

            eventRepository.Verify(_ => _.AddAsync(It.IsAny<Event>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        private static (CreateEventUseCase useCase, Mock<IEventRepository> eventRepository) CreateUseCase()
        {
            var eventRepository = new Mock<IEventRepository>();
            var timeProvider = new Mock<ITimeProvider>();
            timeProvider
                .Setup(_ => _.Now)
                .Returns(DateTime.UtcNow);

            var useCase = new CreateEventUseCase(
                timeProvider.Object,
                eventRepository.Object);

            return (useCase, eventRepository);
        }
    }
}
