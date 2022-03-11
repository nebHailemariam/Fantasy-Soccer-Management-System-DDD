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
    public class UsersController : ControllerBase
    {
        private readonly IRepository<League> _leagueRepository;
        private readonly IIdentityService<ApplicationUser> _userService;

        public UsersController(IRepository<League> leagueRepository,
                               IIdentityService<ApplicationUser> userRepository)
        {
            _leagueRepository = leagueRepository;
            _userService = userRepository;
        }

        [HttpPost("signup/team-manager")]
        public async Task<IActionResult> RegisterTeamManager([FromBody] TeamManagerCreateDto teamManagerCreateDto)
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
            await _userService.RegisterTeamManagerAsync(user, teamManagerCreateDto.Password, existingLeague);

            return NoContent();
        }

        [HttpPost("signup/leagues-manager")]
        public async Task<IActionResult> RegisterLeaguesManager([FromBody] LeaguesManagerCreateDto leagueManagerCreateDto)
        {
            var user = new ApplicationUser(leagueManagerCreateDto.Email);
            await _userService.RegisterLeaguesManagerAsync(user, leagueManagerCreateDto.Password);

            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            return Ok(new { Token = await _userService.LoginAsync(userLoginDto.Email, userLoginDto.Password) });
        }
    }
}
