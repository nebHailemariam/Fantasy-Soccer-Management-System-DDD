using FantasySoccerManagement.Core.Aggregate;
using FantasySoccerManagementSystem.SharedKernel;

namespace FantasySoccerManagement.Core.DomainEvents
{
    public class TeamAddedEvent : BaseDomainEvent
    {
        public TeamAddedEvent(Team team)
        {
            TeamAdded = team;
        }
        public Team TeamAdded { get; set; }
    }
}