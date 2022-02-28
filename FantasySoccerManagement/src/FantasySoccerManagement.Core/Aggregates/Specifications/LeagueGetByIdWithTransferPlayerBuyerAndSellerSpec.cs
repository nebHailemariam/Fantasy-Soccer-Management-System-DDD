using Ardalis.Specification;
using FantasySoccerManagement.Core.Aggregate;

namespace FantasySoccerManagement.Core.AggregateSpecifications
{
    public class LeagueGetByIdWithTransferPlayerBuyerAndSellerSpec : Specification<League>, ISingleResultSpecification
    {
        public LeagueGetByIdWithTransferPlayerBuyerAndSellerSpec(Guid leagueId)
        {
            Query.Where(league => league.Id == leagueId).
              Include(league => league.Transfers).ThenInclude(transfer => transfer.Player).
              Include(league => league.Transfers).ThenInclude(transfer => transfer.Buyer).
              Include(league => league.Transfers).ThenInclude(transfer => transfer.Seller); ;
        }
    }
}
