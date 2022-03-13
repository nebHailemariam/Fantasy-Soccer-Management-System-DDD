using FantasySoccerManagement.Core.Aggregate;
using FantasySoccerManagementSystem.SharedKernel;

namespace FantasySoccerManagement.Core.DomainEvents
{
    public class PlayerAddedEvent : BaseDomainEvent
    {
        public PlayerAddedEvent(Player player)
        {
            PlayerAdded = player;
        }
        public Player PlayerAdded { get; set; }
    }
}