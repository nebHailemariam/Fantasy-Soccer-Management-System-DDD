using FantasySoccerPublic.Entities;
using Microsoft.EntityFrameworkCore;

namespace FantasySoccerPublic.Data
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly AppDbContext _context;

        public PlayerRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ICollection<Player>> GetAsync()
        {
            return await _context.Players.ToListAsync();
        }

        public async Task<Player> CreateAsync(Player newPlayer)
        {
            await _context.Players.AddAsync(newPlayer);
            await _context.SaveChangesAsync();
            return newPlayer;
        }
    }
}