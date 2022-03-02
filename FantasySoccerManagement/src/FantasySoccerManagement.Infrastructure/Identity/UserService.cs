using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using FantasySoccerManagement.Core.Aggregate;
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
            var claims = GetClaimsAsync(user);
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

        public List<Claim> GetClaimsAsync(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
             new Claim("id", user.Id.ToString()), new Claim(ClaimTypes.Email, user.Email)
            };
            return claims;
        }

        public SigningCredentials GetSigningCredentials()
        {
            var jwtSettings = _configuration.GetSection("JWTSettings");
            var key = Encoding.UTF8.GetBytes(jwtSettings.GetSection("securityKey").Value);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }


        public async Task RegisterAsync(ApplicationUser user, string password, League league)
        {
            Console.WriteLine(JsonSerializer.Serialize(user) + "\n\n\n\n\n\n");
            if (password.Length < 6)
            {
                throw new Exception("Password length must be greater than 6 characters");
            }
            if (await _userRepository.IsEmailTakenAsync(user.Email))
            {
                throw new Exception("Email is already registered");
            }

            await CreateAsync(user, password, league);
        }


        public async Task CreateAsync(ApplicationUser user, string password, League league)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var strategy = context.Database.CreateExecutionStrategy();
                await strategy.Execute(async () =>
                {
                    using var transaction = context.Database.BeginTransaction();
                    try
                    {
                        user.UserName = user.Email;
                        user.NormalizedUserName = user.Email.ToUpper();
                        user.NormalizedEmail = user.Email.ToUpper();
                        // Hash password.
                        user.SecurityStamp = Guid.NewGuid().ToString();
                        user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, password);

                        user.CreatedAt = DateTime.UtcNow;

                        await _context.Users.AddAsync(user);
                        await _context.SaveChangesAsync();
                        await _leagueRepository.UpdateAsync(league);
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
}