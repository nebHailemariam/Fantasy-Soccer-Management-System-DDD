using FantasySoccerPublic.Commands;
using FantasySoccerPublic.Data;
using MediatR;

namespace FantasySoccerPublic.Handlers
{
    public class PlayerAddedCommandHandler : IRequestHandler<PlayerAddedCommand>
    {
        private readonly IServiceProvider _serviceProvider;

        public PlayerAddedCommandHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<Unit> Handle(PlayerAddedCommand request, CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var playerRepository = scope.ServiceProvider.GetRequiredService<IPlayerRepository>();
                await playerRepository.CreateAsync(request.PlayerAdded);
            }
            return Unit.Value;
        }
    }
}