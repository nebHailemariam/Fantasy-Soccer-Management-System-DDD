using Ardalis.GuardClauses;
using FantasySoccerManagementSystem.SharedKernel;

namespace FantasySoccerManagement.Core.Aggregate
{
    public class TeamManager : BaseEntity<Guid>
    {
        public TeamManager(Guid id, string firstName, string lastName, Guid leagueId)
        {
            Id = Guard.Against.Default(id, nameof(id));
            FirstName = Guard.Against.NullOrWhiteSpace(firstName, nameof(firstName));
            LastName = Guard.Against.NullOrWhiteSpace(lastName, nameof(lastName));
            LeagueId = Guard.Against.Default(leagueId, nameof(leagueId));
            CreatedAt = DateTime.UtcNow;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Team> Teams { get; set; }
        public Guid LeagueId { get; set; }

        public void AddTeam(Team team)
        {
            Guard.Against.Null(team, nameof(team));
            Guard.Against.Default(team.Id, nameof(team.Id));
            Guard.Against.DuplicateTeam(Teams, team, nameof(team));
            team.Id = Guid.Empty;
            Teams.Add(team);
        }

        public void RemoveTeam(Guid teamId)
        {
            Guard.Against.TeamNotFound(Teams, teamId, nameof(teamId));
            Teams.Remove(Teams.Single(team => team.Id == teamId));
        }
    }
}