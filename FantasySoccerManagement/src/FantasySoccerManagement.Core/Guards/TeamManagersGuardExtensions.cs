using FantasySoccerManagement.Core.Aggregate;

namespace Ardalis.GuardClauses
{
    public static class TeamManagersGuardExtensions
    {
        public static void DuplicateTeamManager(this IGuardClause guardClause, List<TeamManager> existingTeamManagers, TeamManager newTeamManager, string parameterName)
        {
            if (existingTeamManagers.Any(a => a.Id == newTeamManager.Id))
            {
                throw new ArgumentException("Cannot add duplicate team manager.", parameterName);
            }
        }

        public static void DuplicateTeam(this IGuardClause guardClause, List<Team> teams, Team newTeam, string parameterName)
        {
            if (teams.Any(team => team.Id == newTeam.Id))
            {
                throw new ArgumentException("Cannot add duplicate team manager.", parameterName);
            }
        }

        public static void TeamManagerNotFound(this IGuardClause guardClause, List<TeamManager> existingTeamManagers, TeamManager teamManager, string parameterName)
        {
            if (existingTeamManagers.Any(a => a.Id != teamManager.Id))
            {
                throw new ArgumentException("Team manager not found.", parameterName);
            }
        }

        public static void TeamNotFound(this IGuardClause guardClause, List<Team> teams, Guid existingTeamId, string parameterName)
        {
            if (!teams.Any(team => team.Id == existingTeamId))
            {
                throw new ArgumentException("Cannot add duplicate team manager.", parameterName);
            }
        }
    }
}
