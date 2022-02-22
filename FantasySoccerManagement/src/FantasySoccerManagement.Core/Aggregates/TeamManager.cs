using Ardalis.GuardClauses;
using FantasySoccerManagementSystem.SharedKernel;
using FantasySoccerManagementSystem.SharedKernel.Interfaces;

namespace FantasySoccerManagement.Core.Aggregate
{
    public class TeamManager : BaseEntity<Guid>
    {
        public TeamManager(Guid id, string firstName, string lastName)
        {
            Id = Guard.Against.Default(id, nameof(id));
            FirstName = Guard.Against.NullOrWhiteSpace(firstName, nameof(firstName));
            LastName = Guard.Against.NullOrWhiteSpace(lastName, nameof(lastName));
            CreatedAt = DateTime.UtcNow;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? TeamId { get; set; }
        public Team Team { get; set; }

        public Team AssignTeam(Team team)
        {
            Guard.Against.Null(team, nameof(team));
            Guard.Against.Default(team.Id, nameof(team.Id));
            TeamId = team.Id;
            Team = team;
            return team;
        }
    }
}