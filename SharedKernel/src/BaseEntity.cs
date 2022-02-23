namespace FantasySoccerManagementSystem.SharedKernel
{
    // Apply this marker interface only to aggregate root entities
    // Repositories will only work with aggregate roots, not their children
    public abstract class BaseEntity<TId>
    {
        public TId Id { get; set; }
        public List<BaseDomainEvent> Events;
    }
}