using FantasySoccerManagement.Core.DomainEvents;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace FantasySoccerManagement.Infrastructure.Messaging
{
    public class TeamAddedEventPublisher : PublisherBase<TeamAddedEvent>
    {
        public TeamAddedEventPublisher(
            ConnectionFactory connectionFactory,
            ILogger<RabbitMqClientBase> logger,
            ILogger<PublisherBase<TeamAddedEvent>> publisherBase) :
            base(connectionFactory, logger, publisherBase)
        {
        }
        protected override string Exchange => "FantasySoccerManagement.Exchange";
        protected override string Queue => "FantasySoccerManagement.Team.Exchange";
        protected override string QueueAndExchangeRoutingKey => "FantasySoccerManagement.Team.Exchange";
        protected override string AppId => "FantasySoccerManagement";

    }
}