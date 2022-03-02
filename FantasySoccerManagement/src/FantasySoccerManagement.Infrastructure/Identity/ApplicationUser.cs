using FantasySoccerManagement.Core.Aggregate;
using Microsoft.AspNetCore.Identity;

namespace FantasySoccerManagement.Infrastructure.Entity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser(string email)
        {
            Id = Guid.NewGuid();
            Email = email;
        }
        public DateTime CreatedAt { get; set; }
        public TeamManager TeamManager { get; set; }
    }
}