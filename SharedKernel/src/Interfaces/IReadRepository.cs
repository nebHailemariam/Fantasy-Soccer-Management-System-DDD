using Ardalis.Specification;

namespace FantasySoccerManagementSystem.SharedKernel.Interfaces
{
    public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
    {
    }
}