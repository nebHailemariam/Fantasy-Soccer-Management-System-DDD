using Microsoft.AspNetCore.Mvc;
using FantasySoccerManagementSystem.SharedKernel.Interfaces;
using FantasySoccerManagement.Core.Aggregate;

namespace FantasySoccerManagement.Api.TeamManagersEndpoints
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManagersController : ControllerBase
    {
        private readonly IReadRepository<Managers> _maragersRepository;

        public ManagersController(IReadRepository<Managers> managersRepository)
        {
            _maragersRepository = managersRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _maragersRepository.ListAsync();
            return Ok(response);
        }
    }
}
