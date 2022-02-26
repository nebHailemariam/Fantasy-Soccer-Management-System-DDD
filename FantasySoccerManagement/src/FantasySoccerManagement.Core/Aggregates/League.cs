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

        public void TransferTeamOwnership(TeamManager previousTeamOwner, Team teamToBeTransferred, TeamManager newTeamOwner)
        {
            Guard.Against.Null(previousTeamOwner, nameof(previousTeamOwner));
            Guard.Against.TeamManagerNotFound(TeamManagers, previousTeamOwner, nameof(previousTeamOwner));
            Guard.Against.Null(newTeamOwner, nameof(newTeamOwner));
            Guard.Against.TeamManagerNotFound(TeamManagers, newTeamOwner, nameof(newTeamOwner));
            Guard.Against.TeamNotFound(previousTeamOwner.Teams, teamToBeTransferred.Id, nameof(teamToBeTransferred.Id));

            previousTeamOwner.RemoveTeam(teamToBeTransferred.Id);
            teamToBeTransferred.TeamManagerId = newTeamOwner.Id;
            newTeamOwner.AddTeam(teamToBeTransferred);
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