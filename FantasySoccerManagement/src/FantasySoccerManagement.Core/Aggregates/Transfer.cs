using Ardalis.GuardClauses;
using FantasySoccerManagement.Core.Enums;
using FantasySoccerManagementSystem.SharedKernel;

namespace FantasySoccerManagement.Core.Aggregate
{
    public class Transfer : BaseEntity<Guid>
    {
        public Transfer(Guid id, Guid sellerId, Guid playerId, double askingPrice, Guid leagueId)
        {
            Id = Guard.Against.Default(id, nameof(id));
            SellerId = Guard.Against.Default(sellerId, nameof(sellerId));
            BuyerId = null;
            PlayerId = Guard.Against.Default(playerId, nameof(playerId));
            AskingPrice = Guard.Against.Negative(askingPrice, nameof(askingPrice));
            PlayerTransferStatus = PlayerTransferStatusType.Listed;
            LeagueId = Guard.Against.Default(leagueId, nameof(leagueId));
            CreatedAt = DateTime.UtcNow;
        }

        public Guid SellerId { get; set; }
        public TeamManager Seller { get; set; }
        public Guid? BuyerId { get; set; }
        public TeamManager Buyer { get; set; }
        public Guid PlayerId { get; set; }
        public Player Player { get; set; }
        public double AskingPrice { get; set; }
        public PlayerTransferStatusType PlayerTransferStatus { get; set; }
        public Guid LeagueId { get; set; }
        public DateTime CreatedAt { get; set; }

        public void BuyPlayer(Guid buyerId)
        {
            BuyerId = Guard.Against.Default(buyerId, nameof(buyerId));
            PlayerTransferStatus = PlayerTransferStatusType.Sold;
        }
    }
}