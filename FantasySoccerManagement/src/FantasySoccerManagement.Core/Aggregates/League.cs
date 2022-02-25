using Ardalis.GuardClauses;
using FantasySoccerManagementSystem.SharedKernel;
using FantasySoccerManagementSystem.SharedKernel.Interfaces;

namespace FantasySoccerManagement.Core.Aggregate
{
    public class League : BaseEntity<Guid>, IAggregateRoot
    {
        public League(Guid id, string name)
        {
            Id = Guard.Against.Default(id, nameof(id));
            Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
        }
        public string Name { get; set; }

        public List<TeamManager> TeamManagers { get; set; }

        public void AddTeamManager(TeamManager teamManager)
        {
            Guard.Against.Null(teamManager, nameof(teamManager));
            Guard.Against.DuplicateTeamManager(TeamManagers, teamManager, nameof(teamManager));
            teamManager.Id = Guid.Empty;
            TeamManagers.Add(teamManager);
        }

        public void TransferTeamOwnership(TeamManager previousTeamManager, TeamManager newTeamManager)
        {
            Guard.Against.Null(previousTeamManager, nameof(previousTeamManager));
            Guard.Against.TeamManagerNotFound(TeamManagers, previousTeamManager, nameof(previousTeamManager));
            Guard.Against.Null(newTeamManager, nameof(newTeamManager));
            Guard.Against.TeamManagerNotFound(TeamManagers, newTeamManager, nameof(newTeamManager));
            Guard.Against.TeamManagerHasNoTeam(previousTeamManager, nameof(previousTeamManager));
            Guard.Against.TeamManagerHasTeam(newTeamManager, nameof(newTeamManager));

            // newTeamManager.Team = newTeamManager.Team;
            // previousTeamManager.Team = null;
        }


        public void DeleteTeamManager(TeamManager teamManager)
        {
            Guard.Against.Null(teamManager, nameof(teamManager));
            var teamManagerToDelete = TeamManagers
                                      .Where(t => t.Id == teamManager.Id)
                                      .FirstOrDefault();

            if (teamManagerToDelete != null)
            {
                TeamManagers.Remove(teamManagerToDelete);
            }
        }
    }
}