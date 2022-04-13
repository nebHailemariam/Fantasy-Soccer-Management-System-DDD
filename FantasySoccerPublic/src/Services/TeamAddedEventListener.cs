﻿using FantasySoccerPublic.Commands;
using FantasySoccerPublic.Helpers.Messaging;
using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Rmq.Common;

namespace FantasySoccerPublic.Services
{
    public class TeamAddedEventListener : ConsumerBase, IHostedService
    {
        protected override string Exchange => "FantasySoccerManagement.Exchange";
        protected override string Queue => "FantasySoccerManagement.Team.Exchange";
        protected override string QueueAndExchangeRoutingKey => "FantasySoccerManagement.Team.Exchange";

        public TeamAddedEventListener(
            IMediator mediator,
            ConnectionFactory connectionFactory,
            ILogger<PlayerAddedEventListener> PlayerAddedEventListenerLogger,
            ILogger<ConsumerBase> consumerLogger,
            ILogger<RabbitMqClientBase> logger) :
            base(mediator, connectionFactory, consumerLogger, logger)
        {
            try
            {
                var consumer = new AsyncEventingBasicConsumer(Channel);
                consumer.Received += OnEventReceived<TeamAddedCommand>;
                Channel.BasicConsume(queue: Queue, autoAck: false, consumer: consumer);
            }
            catch (Exception ex)
            {
                PlayerAddedEventListenerLogger.LogCritical(ex, "Error while consuming message");
            }
        }

        public virtual Task StartAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        public virtual Task StopAsync(CancellationToken cancellationToken)
        {
            Dispose();
            return Task.CompletedTask;
        }
    }
}