using Ardalis.GuardClauses;
using FantasySoccerManagementSystem.SharedKernel;
using FantasySoccerManagementSystem.SharedKernel.Interfaces;

namespace FantasySoccerManagement.Core.Aggregate
{
    public class League : BaseEntity<Guid>, IAggregateRoot
    {
        public League(Guid id)
        {
            Id = Guard.Against.Default(id, nameof(id));
        }

        public virtual List<TeamManager> TeamManagers { get; set; }

        public void AddTeamManager(TeamManager teamManager)
        {
            Guard.Against.Null(teamManager, nameof(teamManager));
            Guard.Against.DuplicateTeamManager(TeamManagers, teamManager, nameof(teamManager));
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

            newTeamManager.TeamId = previousTeamManager.Id;
            newTeamManager.Team = newTeamManager.Team;
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