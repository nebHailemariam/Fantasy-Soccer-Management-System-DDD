using FantasySoccerManagement.Core.Aggregate;

namespace Ardalis.GuardClauses
{
    public static class TeamsGuardExtensions
    {
        public static void DuplicatePlayer(this IGuardClause guardClause, List<Player> players, Player newPlayer, string parameterName)
        {
            if (players.Any(a => a.Id == newPlayer.Id))
            {
                throw new ArgumentException("Cannot add duplicate player.", parameterName);
            }
        }
    }
}
