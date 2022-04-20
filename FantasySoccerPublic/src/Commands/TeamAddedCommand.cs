using FantasySoccerPublic.Entities;
using MediatR;

namespace FantasySoccerPublic.Commands
{
    public class TeamAddedCommand : IRequest
    {
        public Team TeamAdded { get; set; }
    }
}