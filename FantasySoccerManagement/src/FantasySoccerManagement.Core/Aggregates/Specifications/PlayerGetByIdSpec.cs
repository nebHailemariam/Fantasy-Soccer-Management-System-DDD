using Ardalis.Specification;
using FantasySoccerManagement.Core.Aggregate;

namespace FantasySoccerManagement.Core.AggregateSpecifications
{
    public class PlayerGetByIdWithTransfersSpec : Specification<League>, ISingleResultSpecification
    {
        public PlayerGetByIdWithTransfersSpec(Guid playerId)
        {
            Query.Where(league => league.TeamManagers.Count != 0).Include(league => league.Transfers).
            Where(league => league.TeamManagers.Where(teamManager => teamManager.Teams.Count != 0 && teamManager.Teams.Where(team => team.Players.Count != 0 && team.Players.Where(player => player.Id == playerId).Any()).Any()).Any()).
            Include(league => league.TeamManagers.Where(teamManager => teamManager.Teams.Count != 0 && teamManager.Teams.Where(team => team.Players.Count != 0 && team.Players.Where(player => player.Id == playerId).Any()).Any())).
            ThenInclude(teamManager => teamManager.Teams.Where(team => team.Players.Count != 0 && team.Players.Where(player => player.Id == playerId).Any())).
            ThenInclude(team => team.Players.Where(player => player.Id == playerId));
        }
    }
}