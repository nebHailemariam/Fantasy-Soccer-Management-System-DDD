using Ardalis.Specification.EntityFrameworkCore;
using FantasySoccerManagementSystem.SharedKernel.Interfaces;

namespace FantasySoccerManagement.Infrastructure.Data
{
    public class EfRepository<T> : RepositoryBase<T>, IRepository<T> where T : class, IAggregateRoot
    {
        public EfRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}