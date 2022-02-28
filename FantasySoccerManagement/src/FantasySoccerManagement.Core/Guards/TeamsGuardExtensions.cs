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

        public static void PlayerNotFound(this IGuardClause guardClause, List<Player> players, Guid playerId, string parameterName)
        {
            if (!players.Any(a => a.Id == playerId))
            {
                throw new ArgumentException("Player not found", parameterName);
            }
        }

        public static void MaximumTimeSizeExceeded(this IGuardClause guardClause, List<Player> players, string parameterName)
        {
            if (players.Count >= 20)
            {
                throw new ArgumentException("Cannot add another player. Team size limit cannot exceed 20 players.", parameterName);
            }
        }

        public static void IdenticalPlayerBuyerAndSellerTeam(this IGuardClause guardClause, Guid team1, Guid team2, string parameterName)
        {
            if (team1 == team2)
            {
                throw new ArgumentException("You cannot by your own player.", parameterName);
            }
        }
    }
}
