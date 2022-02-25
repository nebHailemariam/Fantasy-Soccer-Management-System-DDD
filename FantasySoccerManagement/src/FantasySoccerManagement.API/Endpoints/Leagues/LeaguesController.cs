using Microsoft.AspNetCore.Mvc;
using FantasySoccerManagementSystem.SharedKernel.Interfaces;
using FantasySoccerManagement.Core.Aggregate;
using FantasySoccerManagement.Api.Dtos;
using AutoMapper;

namespace FantasySoccerManagement.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaguesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepository<League> _leagueRepository;
        private readonly IReadRepository<League> _leagueCachedRepository;

        public LeaguesController(IMapper mapper, IReadRepository<League> leagueCachedRepository, IRepository<League> leagueRepository)
        {
            _mapper = mapper;
            _leagueCachedRepository = leagueCachedRepository;
            _leagueRepository = leagueRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _leagueCachedRepository.ListAsync();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LeagueCreateDto leagueCreateDto)
        {
            var league = new League(Guid.NewGuid(), leagueCreateDto.Name);
            var createLeague = await _leagueRepository.AddAsync(league);

            return Ok(createLeague);
        }
    }
}
