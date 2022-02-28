using Ardalis.Specification;
using FantasySoccerManagement.Core.Aggregate;

namespace FantasySoccerManagement.Core.AggregateSpecifications
{
    public class TransferGetByIdSpec : Specification<League>, ISingleResultSpecification
    {
        public TransferGetByIdSpec(Guid transferId)
        {
            Query.
            Where(league => league.Transfers.Count != 0 && league.Transfers.Where(transfer => transfer.Id == transferId).Any()).
            Include(league => league.Transfers.Where(transfer => transfer.Id == transferId)).
            ThenInclude(transfer => transfer.Player).
            Include(league => league.Transfers.Where(transfer => transfer.Id == transferId)).
            ThenInclude(transfer => transfer.Buyer).
            Include(league => league.Transfers.Where(transfer => transfer.Id == transferId)).
            ThenInclude(transfer => transfer.Seller);
        }
    }
}
