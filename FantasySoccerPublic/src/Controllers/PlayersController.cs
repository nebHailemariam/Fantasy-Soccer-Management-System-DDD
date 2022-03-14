using FantasySoccerPublic.Services;
using Microsoft.AspNetCore.Mvc;

namespace FantasySoccerPublic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerService _playerService;
        public PlayersController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var players = await _playerService.GetAsync();
            return Ok(players);
        }
    }
}