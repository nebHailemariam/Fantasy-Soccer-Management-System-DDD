namespace FantasySoccerManagement.Api.Dtos
{
    public class TeamManagerCreateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid LeagueId { get; set; }
    }
}