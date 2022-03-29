using FantasySoccerPublic.Entities;

namespace FantasySoccerPublic.Data
{
    public interface ITeamRepository
    {
        Task<ICollection<Team>> GetAsync();
        Task<Team> CreateAsync(Team newTeam);
    }
}