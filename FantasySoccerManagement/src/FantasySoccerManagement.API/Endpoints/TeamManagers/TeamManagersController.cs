using Microsoft.AspNetCore.Mvc;
using FantasySoccerManagementSystem.SharedKernel.Interfaces;
using FantasySoccerManagement.Core.Aggregate;
using FantasySoccerManagement.Api.Dtos;
using FantasySoccerManagement.Core.AggregateSpecifications;
using FantasySoccerManagement.Infrastructure.Entity;
using FantasySoccerManagement.Infrastructure.Interfaces;

namespace FantasySoccerManagement.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamManagersController : ControllerBase
    {
        private readonly IRepository<League> _leagueRepository;
        private readonly IReadRepository<League> _leagueCachedRepository;
        private readonly IIdentityService<ApplicationUser> _userService;

        public TeamManagersController(IReadRepository<League> leagueCachedRepository,
                                      IRepository<League> leagueRepository,
                                      IIdentityService<ApplicationUser> userRepository)
        {
            _leagueCachedRepository = leagueCachedRepository;
            _leagueRepository = leagueRepository;
            _userService = userRepository;
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TeamManagerCreateDto teamManagerCreateDto)
        {
            var spec = new LeagueGetByIdWithTeamManagersSpec(teamManagerCreateDto.LeagueId);
            var existingLeague = await _leagueRepository.GetBySpecAsync(spec);
            if (existingLeague == null)
            {
                return NotFound(new { message = "League not found" });
            }
            var user = new ApplicationUser(teamManagerCreateDto.Email);
            var teamManager = new TeamManager(Guid.NewGuid(), teamManagerCreateDto.FirstName, teamManagerCreateDto.LastName, teamManagerCreateDto.LeagueId, user.Id);
            existingLeague.AddTeamManager(teamManager);
            await _userService.RegisterAsync(user, teamManagerCreateDto.Password, existingLeague);

            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            return Ok(new { Token = await _userService.LoginAsync(userLoginDto.Email, userLoginDto.Password) });
        }
    }
}
