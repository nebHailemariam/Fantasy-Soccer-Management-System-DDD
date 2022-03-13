namespace FantasySoccerManagement.Core.Interfaces
{
    public interface IMessagePublisher<in T>
    {
        void Publish(T @event);
    }
}