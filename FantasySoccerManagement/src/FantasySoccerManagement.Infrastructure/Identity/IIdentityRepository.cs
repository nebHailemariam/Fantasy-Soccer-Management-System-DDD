using FantasySoccerManagement.Infrastructure.Data;

namespace FantasySoccerManagement.Infrastructure.Interfaces
{
    public interface IIdentityRepository<T1>
    {
        Task<bool> CheckPasswordAsync(T1 user, string password);
        Task<T1> CreateAsync(AppDbContext context, T1 user, string password);
        Task<bool> IsEmailTakenAsync(string email);
        Task<T1> GetByEmailAsync(string email);
        Task<T1> GetByIdAsync(string id);
    }
}
