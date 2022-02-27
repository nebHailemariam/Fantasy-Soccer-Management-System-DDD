using Ardalis.GuardClauses;
using FantasySoccerManagementSystem.SharedKernel;

namespace FantasySoccerManagement.Core.Aggregate
{
    public class Team : BaseEntity<Guid>
    {
        public Team(Guid id, string name, string country, Guid teamManagerId)
        {
            Id = Guard.Against.Default(id, nameof(id));
            Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
            Country = Guard.Against.InvalidCountry(country, nameof(country));
            TeamValue = 0;
            Money = 5000000;
            TeamManagerId = Guard.Against.Default(teamManagerId, nameof(teamManagerId));
            CreatedAt = DateTime.UtcNow;
        }

        public string Name { get; set; }
        public string Country { get; set; }
        public double TeamValue { get; set; }
        public double Money { get; set; }
        public Guid TeamManagerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual List<Player> Players { get; set; }

        public void AddPlayer(Player player)
        {
            Guard.Against.MaximumTimeSizeExceeded(Players, nameof(Players));
            Guard.Against.DuplicatePlayer(Players, player, nameof(player));
            player.Id = Guid.Empty;
            TeamValue += player.Value;
            Players.Add(player);
        }
    }
}