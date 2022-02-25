using Ardalis.GuardClauses;
using FantasySoccerManagementSystem.SharedKernel;
using FantasySoccerManagementSystem.SharedKernel.Interfaces;

namespace FantasySoccerManagement.Core.Aggregate
{
    public class Team : BaseEntity<Guid>
    {
        public Team(Guid id, string name, string country, double teamValue, double money, Guid teamManagerId)
        {
            Id = Guard.Against.Default(id, nameof(id));
            Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
            Country = Guard.Against.InvalidCountry(country, nameof(country));
            TeamValue = Guard.Against.Negative(teamValue, nameof(teamValue));
            Money = Guard.Against.Zero(money, nameof(money));
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
    }
}