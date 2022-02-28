using Ardalis.Specification;
using FantasySoccerManagement.Core.Aggregate;

namespace FantasySoccerManagement.Core.AggregateSpecifications
{
    public class LeagueGetByIdWithTransferAndPlayersSpec : Specification<League>, ISingleResultSpecification
    {
        public LeagueGetByIdWithTransferAndPlayersSpec(Guid leagueId)
        {
            Query
              .Where(league => league.Id == leagueId)
              .Include(league => league.Transfers)
              .Include(league => league.TeamManagers)
              .ThenInclude(teamManager => teamManager.Teams)
              .ThenInclude(team => team.Players);
        }
    }
}
