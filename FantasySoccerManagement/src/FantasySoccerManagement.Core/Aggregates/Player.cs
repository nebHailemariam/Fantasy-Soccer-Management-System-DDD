using Ardalis.GuardClauses;
using FantasySoccerManagement.Core.Enums;
using FantasySoccerManagementSystem.SharedKernel;
using FantasySoccerManagementSystem.SharedKernel.Interfaces;

namespace FantasySoccerManagement.Core.Aggregate
{
    public class Player : BaseEntity<Guid>
    {
        public Player(Guid id, string firstName, string lastName, string country, double value, Guid teamId)
        {
            Id = Guard.Against.Default(id, nameof(id));
            FirstName = Guard.Against.NullOrWhiteSpace(firstName, nameof(firstName));
            LastName = Guard.Against.NullOrWhiteSpace(lastName, nameof(lastName));
            Country = Guard.Against.InvalidCountry(country, nameof(country));
            Value = Guard.Against.Negative(value, nameof(value));
            TeamId = Guard.Against.Default(teamId, nameof(teamId));
            CreatedAt = DateTime.UtcNow;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public PlayerPositionType PlayerPositionType { get; set; }
        public DateTime DateOfBirth { get; set; }
        public double Value { get; set; }
        public Guid TeamId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}