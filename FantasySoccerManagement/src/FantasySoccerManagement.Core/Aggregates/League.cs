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
        public List<Transfer> Transfers { get; set; }

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

        public void ListPlayerOnMarkatPlace(Transfer transfer)
        {
            Guard.Against.DuplicatePlayerTransfer(Transfers, transfer.PlayerId, nameof(transfer.PlayerId));
            transfer.Id = Guid.Empty;
            Transfers.Add(transfer);
        }

        public void BuyPlayer(Guid playerSellerTeamId, Guid playerBuyerTeamId, Guid playerId, Guid transferId, double askingPrice)
        {
            Guard.Against.IdenticalPlayerBuyerAndSellerTeam(playerSellerTeamId, playerBuyerTeamId, nameof(playerSellerTeamId));
            var player = TeamManagers.Where(teamManager => teamManager.Teams.Where(team => team.Players.Where(player => player.Id == playerId).Any()).Any()).FirstOrDefault().Teams.FirstOrDefault().Players.Find(p => p.Id == playerId);

            var sellerPlayerTeam = TeamManagers.Where(teamManager => teamManager.Teams.Where(team => team.Id == playerSellerTeamId).Any()).FirstOrDefault().Teams.FirstOrDefault();
            sellerPlayerTeam.SellPlayer(player, askingPrice);

            var BuyerPlayerTeam = TeamManagers.Where(teamManager => teamManager.Teams.Where(team => team.Id == playerBuyerTeamId).Any()).FirstOrDefault().Teams.FirstOrDefault();
            BuyerPlayerTeam.BuyPlayer(player, askingPrice);

            var transfer = Transfers.Find(t => t.Id == transferId);
            transfer.BuyPlayer(BuyerPlayerTeam.TeamManagerId);
        }
    }
}