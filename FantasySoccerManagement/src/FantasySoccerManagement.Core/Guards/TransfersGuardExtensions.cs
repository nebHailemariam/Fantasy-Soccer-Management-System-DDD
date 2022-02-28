using FantasySoccerManagement.Core.Aggregate;
using FantasySoccerManagement.Core.Enums;

namespace Ardalis.GuardClauses
{
    public static class TransfersGuardExtensions
    {
        public static void DuplicatePlayerTransfer(this IGuardClause guardClause, List<Transfer> transfers, Guid playerToBeTransferedId, string parameterName)
        {
            if (transfers.Any(transfer => transfer.Player.Id == playerToBeTransferedId && transfer.PlayerTransferStatus == PlayerTransferStatusType.Listed))
            {
                throw new ArgumentException("Player is already on the marketlist.", parameterName);
            }
        }
    }
}
