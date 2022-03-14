using FantasySoccerManagementSystem.SharedKernel;
using FantasySoccerPublic.Enums;

namespace FantasySoccerPublic.Entities
{
    public class Player : BaseEntity<Guid>
    {
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