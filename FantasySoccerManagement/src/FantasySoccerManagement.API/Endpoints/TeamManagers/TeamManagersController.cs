using Microsoft.AspNetCore.Mvc;
using FantasySoccerManagementSystem.SharedKernel.Interfaces;
using FantasySoccerManagement.Core.Aggregate;
using FantasySoccerManagement.Api.Dtos;
using AutoMapper;
using FantasySoccerManagement.Core.AggregateSpecifications;
using System.Text.Json;

namespace FantasySoccerManagement.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamManagersController : ControllerBase
    {
        private readonly IRepository<League> _leagueRepository;
        private readonly IReadRepository<League> _leagueCachedRepository;

        public TeamManagersController(IReadRepository<League> leagueCachedRepository, IRepository<League> leagueRepository)
        {
            _leagueCachedRepository = leagueCachedRepository;
            _leagueRepository = leagueRepository;
        }

        [HttpGet("{teamManagerId}")]
        public async Task<IActionResult> Get([FromRoute] Guid teamManagerId)
        {
            var spec = new TeamManagerGetByIdWithPalyersSpec(teamManagerId);
            var response = await _leagueCachedRepository.GetBySpecAsync(spec);
            return Ok(response);
        }

        [HttpGet("league/{leagueId}")]
        public async Task<IActionResult> GetAll([FromRoute] Guid leagueId)
        {
            var spec = new LeagueGetByIdWithTeamManagersSpec(leagueId);
            var response = await _leagueCachedRepository.GetBySpecAsync(spec);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TeamManagerCreateDto teamManagerCreateDto)
        {
            var spec = new LeagueGetByIdWithTeamManagersSpec(teamManagerCreateDto.LeagueId);
            var existingLeague = await _leagueRepository.GetBySpecAsync(spec);
            if (existingLeague == null)
            {
                return NotFound(new { message = "League not found" });
            }
            var teamManager = new TeamManager(Guid.NewGuid(), teamManagerCreateDto.FirstName, teamManagerCreateDto.LastName, teamManagerCreateDto.LeagueId);

            existingLeague.AddTeamManager(teamManager);
            await _leagueRepository.UpdateAsync(existingLeague);

            return NoContent();
        }
    }
}
