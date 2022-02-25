using Ardalis.Specification;
using FantasySoccerManagement.Core.Aggregate;

namespace FantasySoccerManagement.Core.AggregateSpecifications
{
    public class TeamManagerGetByIdWithPalyersSpec : Specification<League>, ISingleResultSpecification
    {
        public TeamManagerGetByIdWithPalyersSpec(Guid teamManagerId)
        {
            Query.Include(league => league.TeamManagers).ThenInclude(teamManager => teamManager.Team).ThenInclude(team => team.Players).
            Where(league => league.TeamManagers.Any(teamManager => teamManager.Id == teamManagerId));


        }
    }
}
