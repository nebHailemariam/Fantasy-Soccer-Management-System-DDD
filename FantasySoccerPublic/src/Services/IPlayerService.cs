using FantasySoccerPublic.Entities;

namespace FantasySoccerPublic.Services
{
    public interface IPlayerService
    {
        Task<ICollection<Player>> GetAsync();
    }
}