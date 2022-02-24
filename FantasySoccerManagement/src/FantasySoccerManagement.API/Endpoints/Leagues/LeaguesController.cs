using Microsoft.AspNetCore.Mvc;
using FantasySoccerManagementSystem.SharedKernel.Interfaces;
using FantasySoccerManagement.Core.Aggregate;

namespace FantasySoccerManagement.Api.LeaguesEndpoints
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaguesController : ControllerBase
    {
        private readonly IReadRepository<League> _leagueRepository;

        public LeaguesController(IReadRepository<League> leagueRepository)
        {
            _leagueRepository = leagueRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _leagueRepository.ListAsync();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var response = await _leagueRepository.ListAsync();

            return Ok(response);
        }
    }
}
