namespace FantasySoccerManagement.Api.Dtos
{
    public class TeamCreateDto
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public Guid TeamManagerId { get; set; }
    }
}