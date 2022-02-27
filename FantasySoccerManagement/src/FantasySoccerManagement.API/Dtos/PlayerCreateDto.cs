using FantasySoccerManagement.Core.Enums;

namespace FantasySoccerManagement.Api.Dtos
{
    public class PlayerCreateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public PlayerPositionType PlayerPositionType { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Guid TeamId { get; set; }
    }
}