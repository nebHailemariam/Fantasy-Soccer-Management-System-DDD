using System.Text.Json;
using FantasySoccerManagement.Core.Aggregate;
using FantasySoccerManagement.Core.DomainEvents;
using MediatR;

namespace FantasySoccerManagement.Core.Handlers
{
    public class RelayPlayerAddedHandler : INotificationHandler<PlayerAddedEvent>
    {
        public async Task Handle(PlayerAddedEvent notification, CancellationToken cancellationToken)
        {
            Console.Write(JsonSerializer.Serialize(notification));
        }
    }

}