using FantasySoccerPublic.Data;
using FantasySoccerPublic.Entities;

namespace FantasySoccerPublic.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<ICollection<Player>> GetAsync()
        {
            return await _playerRepository.GetAsync();
        }
    }
}