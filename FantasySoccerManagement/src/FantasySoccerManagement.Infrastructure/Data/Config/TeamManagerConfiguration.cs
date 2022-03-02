using FantasySoccerManagement.Core.Aggregate;
using FantasySoccerManagement.Infrastructure.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace FantasySoccerManagement.Infrastructure.Data.Config
{
    public class TeamManagerEntityTypeConfiguration : IEntityTypeConfiguration<TeamManager>
    {
        public void Configure(EntityTypeBuilder<TeamManager> builder)
        {
            builder.ToTable("TeamManagers").HasKey(x => x.Id);
            builder.HasIndex(u => u.CreatedBy).IsUnique();

            builder.HasOne<ApplicationUser>().
            WithOne(user => user.TeamManager).
            HasForeignKey<TeamManager>(teamManager => teamManager.CreatedBy).
            OnDelete(DeleteBehavior.Cascade);
        }
    }
}