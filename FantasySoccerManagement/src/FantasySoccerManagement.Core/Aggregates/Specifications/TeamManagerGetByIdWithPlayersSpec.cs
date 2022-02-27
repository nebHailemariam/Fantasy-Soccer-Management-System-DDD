using Ardalis.Specification;
using FantasySoccerManagement.Core.Aggregate;

namespace FantasySoccerManagement.Core.AggregateSpecifications
{
    public class TeamManagerGetByIdWithPlayersSpec : Specification<League>, ISingleResultSpecification
    {
        public TeamManagerGetByIdWithPlayersSpec(Guid teamManagerId)
        {
            Query.Where(league => league.TeamManagers.Count != 0).Include(league => league.TeamManagers.Where(teamManager => teamManager.Id == teamManagerId)).
            ThenInclude(teamManager => teamManager.Teams).ThenInclude(team => team.Players);
        }
    }
}
