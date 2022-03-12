using Microsoft.AspNetCore.Mvc;
using FantasySoccerManagementSystem.SharedKernel.Interfaces;
using FantasySoccerManagement.Core.Aggregate;
using FantasySoccerManagement.Api.Dtos;
using Microsoft.AspNetCore.Authorization;
using FantasySoccerManagement.Infrastructure.Constants;

namespace FantasySoccerManagement.Api
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class LeaguesController : ControllerBase
    {
        private readonly IRepository<League> _leagueRepository;
        private readonly IReadRepository<League> _leagueCachedRepository;

        public LeaguesController(IReadRepository<League> leagueCachedRepository, IRepository<League> leagueRepository)
        {
            _leagueCachedRepository = leagueCachedRepository;
            _leagueRepository = leagueRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var response = await _leagueCachedRepository.ListAsync();
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Policy = PolicyConstants.IS_LEAGUE_MANAGER)]
        public async Task<IActionResult> Create([FromBody] LeagueCreateDto leagueCreateDto)
        {
            var league = new League(Guid.NewGuid(), leagueCreateDto.Name);
            var createLeague = await _leagueRepository.AddAsync(league);

            return Ok(createLeague);
        }
    }
}