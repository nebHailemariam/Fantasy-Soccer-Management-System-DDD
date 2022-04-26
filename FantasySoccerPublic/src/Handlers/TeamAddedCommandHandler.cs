using FantasySoccerPublic.Commands;
using FantasySoccerPublic.Data;
using MediatR;

namespace FantasySoccerPublic.Handlers
{
    public class TeamAddedCommandHandler : IRequestHandler<TeamAddedCommand>
    {
        private readonly IServiceProvider _serviceProvider;

        public TeamAddedCommandHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<Unit> Handle(TeamAddedCommand request, CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var teamRepository = scope.ServiceProvider.GetRequiredService<ITeamRepository>();
                await teamRepository.CreateAsync(request.TeamAdded);
            }
            return Unit.Value;
        }
    }
}