using Microsoft.AspNetCore.Mvc;
using FantasySoccerManagementSystem.SharedKernel.Interfaces;
using FantasySoccerManagement.Core.Aggregate;
using FantasySoccerManagement.Api.Dtos;
using FantasySoccerManagement.Core.AggregateSpecifications;

namespace FantasySoccerManagement.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamsController : ControllerBase
    {
        private readonly IRepository<League> _leagueRepository;
        private readonly IReadRepository<League> _leagueCachedRepository;

        public TeamsController(IReadRepository<League> leagueCachedRepository, IRepository<League> leagueRepository)
        {
            _leagueCachedRepository = leagueCachedRepository;
            _leagueRepository = leagueRepository;
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TeamCreateDto teamCreateDto)
        {
            var spec = new TeamManagerGetByIdWithTeamsSpec(teamCreateDto.TeamManagerId);
            var existingLeague = await _leagueRepository.GetBySpecAsync(spec);
            if (existingLeague == null)
            {
                return NotFound(new { message = "League not found" });
            }
            var existingTeamManager = existingLeague.TeamManagers.FirstOrDefault();

            if (existingTeamManager == null)
            {
                return NotFound(new { message = "Team manager not found" });
            }
            var newTeam = new Team(Guid.NewGuid(), teamCreateDto.Name, teamCreateDto.Country, teamCreateDto.TeamManagerId);
            existingTeamManager.AddTeam(newTeam);

            await _leagueRepository.UpdateAsync(existingLeague);

            return NoContent();
        }
    }
}
