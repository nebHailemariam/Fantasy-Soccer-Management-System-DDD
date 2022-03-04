using FantasySoccerManagement.Infrastructure.Data;
using FantasySoccerManagement.Infrastructure.Entity;
using Microsoft.AspNetCore.Identity;

namespace FantasySoccerManagement.Infrastructure.Interfaces
{
    public interface IIdentityRepository<T1>
    {
        Task AddRoleAsync(AppDbContext context, Guid userId, string role);
        Task<bool> CheckPasswordAsync(T1 user, string password);
        Task<T1> CreateAsync(AppDbContext context, T1 user, string password);
        Task<bool> IsEmailTakenAsync(string email);
        Task<IdentityRole<Guid>> GetApplicationRoleByName(string roleName);
        Task<T1> GetByEmailAsync(string email);
        Task<T1> GetByIdAsync(string id);
        Task<IList<string>> GetRolesAsync(ApplicationUser user);
    }
}
