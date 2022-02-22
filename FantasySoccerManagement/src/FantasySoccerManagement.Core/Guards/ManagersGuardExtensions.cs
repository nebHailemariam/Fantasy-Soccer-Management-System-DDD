using System.Collections.Generic;
using System.Linq;
using FantasySoccerManagement.Core.Aggregate;

namespace Ardalis.GuardClauses
{
    public static class ManagersGuardExtensions
    {
        public static void DuplicateTeamManager(this IGuardClause guardClause, List<TeamManager> existingTeamManagers, TeamManager newTeamManager, string parameterName)
        {
            if (existingTeamManagers.Any(a => a.Id == newTeamManager.Id))
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

        public static void TeamManagerHasNoTeam(this IGuardClause guardClause, TeamManager teamManager, string parameterName)
        {
            if (teamManager.TeamId == null)
            {
                throw new ArgumentException("Team manager does not have a team.", parameterName);
            }
        }

        public static void TeamManagerHasTeam(this IGuardClause guardClause, TeamManager teamManager, string parameterName)
        {
            if (teamManager.TeamId != null)
            {
                throw new ArgumentException("Team manager already has a team.", parameterName);
            }
        }
    }
}
