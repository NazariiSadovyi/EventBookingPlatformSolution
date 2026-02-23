using EBP.Application.Commands;
using EBP.Application.DTOs;
using EBP.Application.UseCases;
using EBP.Domain.Entities;
using EBP.Domain.Exceptions;
using EBP.Domain.Repositories;
using Moq;

namespace EBP.Application.UnitTests.UseCases
{
    public class AddEventTicketUseCaseTests
    {
        [Test]
        public void GivenEventDoesNotExist_ShouldThrowEventNotFoundException()
        {
            var command = new AddEventTicketCommand(Guid.NewGuid(), TicketKindDto.Regular, 10);
            var (useCase, eventRepository) = CreateUseCase();
            eventRepository
                .Setup(_ => _.GetAsync(command.EventId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(default(Event));

            Assert.ThrowsAsync<EventNotFoundException>(() => useCase.Handle(command, CancellationToken.None));
        }

        [Test]
        public async Task GivenEventExists_ShouldAddTicket()
        {
            var command = new AddEventTicketCommand(Guid.NewGuid(), TicketKindDto.Regular, 10);
            var (useCase, eventRepository) = CreateUseCase();
            var @event = Event.CreateNew(string.Empty, default, default, default, [], default);
            eventRepository
                .Setup(_ => _.GetAsync(command.EventId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(@event);

            var result = await useCase.Handle(command, CancellationToken.None);

            Assert.That(result, Is.EqualTo(@event.Tickets.First().Id));
        }

        private static (AddEventTicketUseCase useCase, Mock<IEventRepository> eventRepository) CreateUseCase()
        {
            var dbSessionRepository = new Mock<IDbSessionRepository>();
            dbSessionRepository
                .Setup(_ => _.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            var eventRepository = new Mock<IEventRepository>();
            var useCase = new AddEventTicketUseCase(dbSessionRepository.Object, eventRepository.Object);
            return (useCase, eventRepository);
        }
    }
}