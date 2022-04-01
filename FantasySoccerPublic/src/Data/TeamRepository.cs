using FantasySoccerPublic.Entities;
using Microsoft.EntityFrameworkCore;

namespace FantasySoccerPublic.Data
{
    public class TeamRepository : ITeamRepository
    {
        private readonly AppDbContext _context;

        public TeamRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ICollection<Team>> GetAsync()
        {
            return await _context.Teams.ToListAsync();
        }

        public async Task<Team> CreateAsync(Team newTeam)
        {
            await _context.Teams.AddAsync(newTeam);
            await _context.SaveChangesAsync();
            return newTeam;
        }
    }
}