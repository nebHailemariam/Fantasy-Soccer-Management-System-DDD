using FantasySoccerManagement.Core.Aggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FantasySoccerManagement.Infrastructure.Data.Config
{
    public class ManagersEntityTypeConfiguration : IEntityTypeConfiguration<Managers>
    {
        public void Configure(EntityTypeBuilder<Managers> builder)
        {
            builder.ToTable("Managers").HasKey(x => x.Id);
        }
    }
}