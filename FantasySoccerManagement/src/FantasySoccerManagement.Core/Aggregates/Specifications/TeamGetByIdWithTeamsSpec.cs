using Ardalis.Specification;
using FantasySoccerManagement.Core.Aggregate;

namespace FantasySoccerManagement.Core.AggregateSpecifications
{
    public class TeamGetByIdWithTeamsSpec : Specification<League>, ISingleResultSpecification
    {
        public TeamGetByIdWithTeamsSpec(Guid teamId)
        {
            Query.Where(league => league.TeamManagers.Count != 0).
            Where(league => league.TeamManagers.Where(teamManager => teamManager.Teams.Count != 0 && teamManager.Teams.Where(team => team.Id == teamId).Any()).Any()).
            Include(league => league.TeamManagers.Where(teamManager => teamManager.Teams.Count != 0 && teamManager.Teams.Where(team => team.Id == teamId).Any())).
            ThenInclude(teamManager => teamManager.Teams.Where(team => team.Id == teamId)).ThenInclude(team => team.Players);
        }
    }
}
