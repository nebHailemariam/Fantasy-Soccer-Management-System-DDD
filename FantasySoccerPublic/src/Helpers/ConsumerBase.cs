using System.Text;
using System.Text.Json;
using FantasySoccerPublic.Helpers.Messaging;
using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Rmq.Common
{
    public abstract class ConsumerBase : RabbitMqClientBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ConsumerBase> _logger;

        public ConsumerBase(
            IMediator mediator,
            ConnectionFactory connectionFactory,
            ILogger<ConsumerBase> consumerLogger,
            ILogger<RabbitMqClientBase> logger) :
            base(connectionFactory, logger)
        {
            _mediator = mediator;
            _logger = consumerLogger;
        }

        protected virtual async Task OnEventReceived<T>(object sender, BasicDeliverEventArgs @event)
        {
            try
            {
                var body = Encoding.UTF8.GetString(@event.Body.ToArray());
                var message = JsonSerializer.Deserialize<T>(body);
                await _mediator.Send(message);
                Channel.BasicAck(@event.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                Channel.BasicNack(@event.DeliveryTag, false, true);
                _logger.LogCritical(ex, "Error while retrieving message from queue.");
            }
        }
    }
}
