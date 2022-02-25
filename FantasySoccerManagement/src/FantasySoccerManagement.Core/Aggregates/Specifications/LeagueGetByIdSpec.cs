using Ardalis.Specification;
using FantasySoccerManagement.Core.Aggregate;

namespace FantasySoccerManagement.Core.AggregateSpecifications
{
    public class LeagueGetByIdSpec : Specification<League>, ISingleResultSpecification
    {
        public LeagueGetByIdSpec(Guid leagueId)
        {
            Query
              .Where(league => league.Id == leagueId);
        }
    }
}
