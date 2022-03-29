using Ardalis.GuardClauses;
using FantasySoccerManagementSystem.SharedKernel;

namespace FantasySoccerPublic.Entities
{
    public class Team : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public double TeamValue { get; set; }
        public double Money { get; set; }
        public Guid TeamManagerId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}