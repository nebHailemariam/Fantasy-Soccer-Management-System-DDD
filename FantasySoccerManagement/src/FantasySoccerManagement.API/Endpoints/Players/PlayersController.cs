using Microsoft.AspNetCore.Mvc;
using FantasySoccerManagementSystem.SharedKernel.Interfaces;
using FantasySoccerManagement.Core.Aggregate;
using FantasySoccerManagement.Api.Dtos;
using FantasySoccerManagement.Core.AggregateSpecifications;
using Microsoft.AspNetCore.Authorization;
using FantasySoccerManagement.Infrastructure.Constants;

namespace FantasySoccerManagement.Api
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly IRepository<League> _leagueRepository;

        public PlayersController(IRepository<League> leagueRepository)
        {
            _leagueRepository = leagueRepository;
        }

        [HttpPost]
        [Authorize(Policy = PolicyConstants.IS_TEAM_MANAGER)]
        public async Task<IActionResult> Create([FromBody] PlayerCreateDto playerCreateDto)
        {
            var spec = new TeamGetByIdWithTeamsSpec(playerCreateDto.TeamId);
            var existingLeague = await _leagueRepository.GetBySpecAsync(spec);
            if (existingLeague == null)
            {
                return NotFound(new { message = "League not found" });
            }
            var existingTeamManager = existingLeague.TeamManagers.FirstOrDefault();
            var existingTeam = existingTeamManager.Teams.FirstOrDefault();
            if (existingTeam == null)
            {
                return NotFound(new { message = "Team not found" });
            }
            var newPlayer = new Player(Guid.NewGuid(), playerCreateDto.FirstName, playerCreateDto.LastName,
            playerCreateDto.Country, playerCreateDto.DateOfBirth, playerCreateDto.TeamId);
            existingTeam.AddPlayer(newPlayer);

            await _leagueRepository.UpdateAsync(existingLeague);

            return NoContent();
        }
    }
}