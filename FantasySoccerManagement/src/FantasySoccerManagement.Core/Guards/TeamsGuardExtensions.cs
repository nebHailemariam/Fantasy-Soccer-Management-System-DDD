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

        public static void MaximumTimeSizeExceeded(this IGuardClause guardClause, List<Player> players, string parameterName)
        {
            if (players.Count >= 20)
            {
                throw new ArgumentException("Cannot add another player. Team size limit cannot exceed 20 players.", parameterName);
            }
        }
    }
}
