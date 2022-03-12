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
    public class TransfersController : ControllerBase
    {
        private readonly IReadRepository<League> _leagueCachedRepository;
        private readonly IRepository<League> _leagueRepository;

        public TransfersController(IReadRepository<League> leagueCachedRepository, IRepository<League> leagueRepository)
        {
            _leagueCachedRepository = leagueCachedRepository;
            _leagueRepository = leagueRepository;
        }

        [HttpGet("{leagueId}")]
        public async Task<IActionResult> Get([FromRoute] Guid leagueId)
        {
            var spec = new LeagueGetByIdWithTransferPlayerBuyerAndSellerSpec(leagueId);
            var existingLeague = await _leagueCachedRepository.GetBySpecAsync(spec);
            return Ok(existingLeague);
        }

        [HttpPost]
        [Authorize(Policy = PolicyConstants.IS_TEAM_MANAGER)]
        public async Task<IActionResult> Create([FromBody] TransferCreateDto transferCreateDto)
        {
            var spec = new PlayerGetByIdWithTransfersSpec(transferCreateDto.PlayerId);
            var existingLeague = await _leagueRepository.GetBySpecAsync(spec);
            if (existingLeague == null)
            {
                return NotFound(new { message = "League not found" });
            }
            var existingTeamManager = existingLeague.TeamManagers.FirstOrDefault();
            var existingPlayer = existingLeague.TeamManagers.FirstOrDefault().Teams.FirstOrDefault().Players.FirstOrDefault();
            var newTransfer = new Transfer(Guid.NewGuid(), existingTeamManager.Id, existingPlayer.Id, transferCreateDto.AskingPrice, existingLeague.Id);
            existingLeague.ListPlayerOnMarkatPlace(newTransfer);
            await _leagueRepository.UpdateAsync(existingLeague);

            return NoContent();
        }

        [HttpPost("buy")]
        [Authorize(Policy = PolicyConstants.IS_TEAM_MANAGER)]
        public async Task<IActionResult> Create([FromBody] TransferBuyDto transferBuyDto)
        {
            var spec = new TransferGetByIdSpec(transferBuyDto.TransferId);
            var existingLeague = await _leagueRepository.GetBySpecAsync(spec);
            if (existingLeague == null)
            {
                return NotFound(new { message = "League not found" });
            }
            var findTeamByIdSpec = new TeamGetByIdWithTeamsSpec(transferBuyDto.TeamId);
            var buyersTeam = (await _leagueRepository.GetBySpecAsync(findTeamByIdSpec))?.
            TeamManagers.Where(teamManager => teamManager.Teams != null && teamManager?.Teams.Count != 0 && teamManager.Teams.Where(team => team.Id == transferBuyDto.TeamId).Any())?.
            FirstOrDefault().Teams.FirstOrDefault();
            if (buyersTeam == null)
            {
                return NotFound(new { message = "Team not found" });
            }
            var existingTransfer = existingLeague.Transfers.FirstOrDefault();
            var newSpec = new LeagueGetByIdWithTransferAndPlayersSpec(existingLeague.Id);
            existingLeague = await _leagueRepository.GetBySpecAsync(newSpec);
            existingLeague.BuyPlayer(existingTransfer.Player.TeamId, transferBuyDto.TeamId, existingTransfer.PlayerId,
             existingTransfer.Id, existingTransfer.AskingPrice);

            await _leagueRepository.UpdateAsync(existingLeague);
            return Ok(existingLeague);
        }
    }
}