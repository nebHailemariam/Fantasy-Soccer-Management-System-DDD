using System.Text.Json;
using FantasySoccerManagement.Core.Aggregate;
using FantasySoccerManagement.Infrastructure.Data;
using FantasySoccerManagement.Infrastructure.Entity;
using FantasySoccerManagement.Infrastructure.Interfaces;
using FantasySoccerManagementSystem.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FantasySoccerManagement.Infrastructure.Data
{
    public class UserRepository : IIdentityRepository<ApplicationUser>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<ApplicationUser> CreateAsync(AppDbContext context, ApplicationUser user, string password)
        {
            // Hash password.
            user.SecurityStamp = Guid.NewGuid().ToString();
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, password);

            user.CreatedAt = DateTime.UtcNow;

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> IsEmailTakenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return false;
            }
            return true;
        }

        public async Task<ApplicationUser> GetByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user;
        }

        public async Task<ApplicationUser> GetByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return user;
        }
    }
}