using FantasySoccerManagement.Infrastructure.Entity;
using FantasySoccerManagement.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FantasySoccerManagement.Infrastructure.Data
{
    public class UserRepository : IIdentityRepository<ApplicationUser>
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(AppDbContext context,
                              UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task AddRoleAsync(AppDbContext context, Guid userId, string role)
        {
            var userRole = new IdentityUserRole<Guid>()
            {
                UserId = userId,
                RoleId = (await GetApplicationRoleByName(role)).Id
            };
            await context.UserRoles.AddAsync(userRole);
            await context.SaveChangesAsync();
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

        public async Task<IdentityRole<Guid>> GetApplicationRoleByName(string roleName)
        {
            var role = await _context.Roles.SingleOrDefaultAsync(r => r.Name == roleName);
            if (role == null)
            {
                throw new Exception($"Role {roleName} not found");
            }
            return role;
        }
        public async Task<ApplicationUser> GetByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new Exception($"User with email {email} not found");
            }
            return user;
        }

        public async Task<ApplicationUser> GetByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return user;
        }

        public async Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }
    }
}