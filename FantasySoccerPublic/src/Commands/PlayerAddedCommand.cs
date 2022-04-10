using FantasySoccerPublic.Entities;
using MediatR;

namespace FantasySoccerPublic.Commands
{
    public class PlayerAddedCommand : IRequest
    {
        public Player PlayerAdded { get; set; }
    }
}