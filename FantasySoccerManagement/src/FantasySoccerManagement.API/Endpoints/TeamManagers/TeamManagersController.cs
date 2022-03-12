using Microsoft.AspNetCore.Mvc;
using FantasySoccerManagementSystem.SharedKernel.Interfaces;
using FantasySoccerManagement.Core.Aggregate;
using FantasySoccerManagement.Core.AggregateSpecifications;
using Microsoft.AspNetCore.Authorization;
using FantasySoccerManagement.Infrastructure.Constants;

namespace FantasySoccerManagement.Api
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class TeamManagersController : ControllerBase
    {
        private readonly IReadRepository<League> _leagueCachedRepository;

        public TeamManagersController(IReadRepository<League> leagueCachedRepository)
        {
            _leagueCachedRepository = leagueCachedRepository;
        }

        [HttpGet("{teamManagerId}")]
        public async Task<IActionResult> Get([FromRoute] Guid teamManagerId)
        {
            var spec = new TeamManagerGetByIdWithPlayersSpec(teamManagerId);
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
    }
}