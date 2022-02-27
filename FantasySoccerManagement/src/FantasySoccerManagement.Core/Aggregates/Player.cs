using Ardalis.GuardClauses;
using FantasySoccerManagement.Core.Enums;
using FantasySoccerManagementSystem.SharedKernel;

namespace FantasySoccerManagement.Core.Aggregate
{
    public class Player : BaseEntity<Guid>
    {
        public Player(Guid id, string firstName, string lastName, string country, DateTime dateOfBirth, Guid teamId)
        {
            Id = Guard.Against.Default(id, nameof(id));
            FirstName = Guard.Against.NullOrWhiteSpace(firstName, nameof(firstName));
            LastName = Guard.Against.NullOrWhiteSpace(lastName, nameof(lastName));
            Country = Guard.Against.InvalidCountry(country, nameof(country));
            Value = 1000000;
            DateOfBirth = dateOfBirth;
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