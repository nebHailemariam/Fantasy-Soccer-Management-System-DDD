using FantasySoccerPublic.Entities;

namespace FantasySoccerPublic.Data
{
    public interface IPlayerRepository
    {
        Task<ICollection<Player>> GetAsync();
        Task<Player> CreateAsync(Player newPlayer);
    }
}