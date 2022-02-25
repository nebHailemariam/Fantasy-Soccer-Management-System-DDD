using Ardalis.Specification;
using FantasySoccerManagement.Core.Aggregate;

namespace FantasySoccerManagement.Core.AggregateSpecifications
{
    public class LeagueGetByIdWithTeamManagersSpec : Specification<League>, ISingleResultSpecification
    {
        public LeagueGetByIdWithTeamManagersSpec(Guid leagueId)
        {
            Query
              .Where(league => league.Id == leagueId)
              .Include(league => league.TeamManagers);
        }
    }
}
