using MediatR;

namespace FantasySoccerManagementSystem.SharedKernel
{
    public abstract class BaseIntegrationEvent : INotification
    {
        public DateTimeOffset DateOccurred { get; protected set; } = DateTimeOffset.UtcNow;
    }
}