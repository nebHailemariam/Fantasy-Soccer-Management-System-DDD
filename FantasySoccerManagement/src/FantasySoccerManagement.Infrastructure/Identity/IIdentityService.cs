using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FantasySoccerManagement.Core.Aggregate;
using FantasySoccerManagement.Infrastructure.Entity;
using Microsoft.IdentityModel.Tokens;

namespace FantasySoccerManagement.Infrastructure.Interfaces
{
    public interface IIdentityService<T1>
    {
        Task<string> LoginAsync(string email, string password);
        JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims);
        Task<List<Claim>> GetClaimsAsync(ApplicationUser user);
        SigningCredentials GetSigningCredentials();
        Task RegisterTeamManagerAsync(T1 user, string password, League league);
        Task RegisterLeaguesManagerAsync(ApplicationUser user, string password);
        Task RegisterTeamManagerAsync(ApplicationUser user, string password, League league);
    }
}
