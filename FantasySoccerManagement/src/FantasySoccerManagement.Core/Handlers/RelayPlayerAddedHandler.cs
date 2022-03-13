using FantasySoccerManagement.Core.DomainEvents;
using FantasySoccerManagement.Core.Interfaces;
using MediatR;

namespace FantasySoccerManagement.Core.Handlers
{
    public class RelayPlayerAddedHandler : INotificationHandler<PlayerAddedEvent>
    {
        private readonly IMessagePublisher<PlayerAddedEvent> _publisher;
        public RelayPlayerAddedHandler(IMessagePublisher<PlayerAddedEvent> publisher)
        {
            _publisher = publisher;
        }

        public Task Handle(PlayerAddedEvent playerAddedEvent, CancellationToken cancellationToken)
        {
            _publisher.Publish(playerAddedEvent);
            return Task.CompletedTask;
        }
    }
}