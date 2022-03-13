using System.Text;
using System.Text.Json;
using FantasySoccerManagement.Core.Interfaces;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace FantasySoccerManagement.Infrastructure.Messaging
{
    public abstract class PublisherBase<T> : RabbitMqClientBase, IMessagePublisher<T>
    {
        private readonly ILogger<PublisherBase<T>> _logger;
        protected abstract string AppId { get; }

        protected PublisherBase(
            ConnectionFactory connectionFactory,
            ILogger<RabbitMqClientBase> logger,
            ILogger<PublisherBase<T>> publisherBaseLogger) :
            base(connectionFactory, logger) => _logger = publisherBaseLogger;

        public virtual void Publish(T @event)
        {
            try
            {
                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
                var properties = Channel.CreateBasicProperties();
                properties.AppId = AppId;
                properties.ContentType = "application/json";
                properties.DeliveryMode = 1; // Doesn't persist to disk
                properties.Timestamp = new AmqpTimestamp(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                Channel.BasicPublish(exchange: Exchange, routingKey: QueueAndExchangeRoutingKey, body: body, basicProperties: properties);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Error while publishing");
            }
        }
    }
}