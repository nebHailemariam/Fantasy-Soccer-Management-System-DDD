using FantasySoccerManagement.Core.Aggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FantasySoccerManagement.Infrastructure.Data.Config
{
    public class TeamManagerEntityTypeConfiguration : IEntityTypeConfiguration<TeamManager>
    {
        public void Configure(EntityTypeBuilder<TeamManager> builder)
        {
            builder.ToTable("TeamManagers").HasKey(x => x.Id);
        }
    }
}