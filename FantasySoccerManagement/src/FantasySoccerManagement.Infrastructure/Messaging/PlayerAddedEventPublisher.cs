using FantasySoccerManagement.Core.DomainEvents;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace FantasySoccerManagement.Infrastructure.Messaging
{
    public class PlayerAddedEventPublisher : PublisherBase<PlayerAddedEvent>
    {
        public PlayerAddedEventPublisher(
            ConnectionFactory connectionFactory,
            ILogger<RabbitMqClientBase> logger,
            ILogger<PublisherBase<PlayerAddedEvent>> publisherBase) :
            base(connectionFactory, logger, publisherBase)
        {
        }
        protected override string Exchange => "FantasySoccerManagement.Exchange";
        protected override string Queue => "FantasySoccerManagement.player.Exchange";
        protected override string QueueAndExchangeRoutingKey => "FantasySoccerManagement.player.Exchange";
        protected override string AppId => "FantasySoccerManagement";

    }
}