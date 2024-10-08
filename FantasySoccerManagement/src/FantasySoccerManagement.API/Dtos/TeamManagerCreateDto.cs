namespace FantasySoccerManagement.Api.Dtos
{
    public class TeamManagerCreateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid LeagueId { get; set; }
    }
}