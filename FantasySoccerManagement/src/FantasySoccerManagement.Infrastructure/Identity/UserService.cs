using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FantasySoccerManagement.Core.Aggregate;
using FantasySoccerManagement.Infrastructure.Constants;
using FantasySoccerManagement.Infrastructure.Entity;
using FantasySoccerManagement.Infrastructure.Interfaces;
using FantasySoccerManagementSystem.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FantasySoccerManagement.Infrastructure.Data
{
    public class UserService : IIdentityService<ApplicationUser>
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;
        private readonly IRepository<League> _leagueRepository;
        private readonly IServiceProvider _serviceProvider;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IIdentityRepository<ApplicationUser> _userRepository;

        public UserService(
            IConfiguration configuration,
            AppDbContext context,
            IRepository<League> leagueRepository,
            IServiceProvider serviceProvider,
            UserManager<ApplicationUser> userManager,
            IIdentityRepository<ApplicationUser> userRepository)
        {
            _configuration = configuration;
            _context = context;
            _leagueRepository = leagueRepository;
            _serviceProvider = serviceProvider;
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (!await _userRepository.CheckPasswordAsync(user, password))
            {
                throw new Exception("Password doesn't match");
            }

            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaimsAsync(user);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return token;
        }

        public JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JWTSettings");
            var tokenOptions = new JwtSecurityToken(
            issuer: jwtSettings.GetSection("validIssuer").Value,
            audience: jwtSettings.GetSection("validAudience").Value,
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("expiryInMinutes").Value)),
            signingCredentials: signingCredentials);
            return tokenOptions;
        }

        public async Task<List<Claim>> GetClaimsAsync(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
             new Claim("id", user.Id.ToString()), new Claim(ClaimTypes.Email, user.Email)
            };
            var roles = await _userRepository.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        public SigningCredentials GetSigningCredentials()
        {
            var jwtSettings = _configuration.GetSection("JWTSettings");
            var key = Encoding.UTF8.GetBytes(jwtSettings.GetSection("securityKey").Value);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        public async Task RegisterAsync(ApplicationUser user, string password, League league, string role)
        {
            if (password.Length < 6)
            {
                throw new Exception("Password length must be greater than 6 characters");
            }
            if (await _userRepository.IsEmailTakenAsync(user.Email))
            {
                throw new Exception("Email is already registered");
            }

            await CreateAsync(user, password, league, role);
        }

        public async Task RegisterLeaguesManagerAsync(ApplicationUser user, string password)
        {

            await RegisterAsync(user, password, null, RoleConstants.LEAGUE_MANAGER_ROLE);
        }

        public async Task RegisterTeamManagerAsync(ApplicationUser user, string password, League league)
        {

            await RegisterAsync(user, password, league, RoleConstants.TEAM_MANAGER_ROLE);
        }

        public async Task CreateAsync(ApplicationUser user, string password, League league, string role)
        {
            using var scope = _serviceProvider.CreateScope();
            var strategy = _context.Database.CreateExecutionStrategy();
            await strategy.Execute(async () =>
            {
                using var transaction = _context.Database.BeginTransaction();
                try
                {
                    user.UserName = user.Email;
                    user.NormalizedUserName = user.Email.ToUpper();
                    user.NormalizedEmail = user.Email.ToUpper();
                    // Hash password.
                    user.SecurityStamp = Guid.NewGuid().ToString();
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, password);

                    user.CreatedAt = DateTime.UtcNow;

                    var createdUser = await _userRepository.CreateAsync(_context, user, password);
                    await _userRepository.AddRoleAsync(_context, createdUser.Id, role);
                    await _context.SaveChangesAsync();
                    if (league != null)
                    {
                        await _leagueRepository.UpdateAsync(league);
                    }
                    transaction.Commit();
                    return user;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            });
        }
    }
}