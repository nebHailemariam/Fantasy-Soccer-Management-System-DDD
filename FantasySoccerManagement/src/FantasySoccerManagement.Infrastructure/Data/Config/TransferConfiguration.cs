using FantasySoccerManagement.Core.Aggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FantasySoccerManagement.Infrastructure.Data.Config
{
    public class TransferEntityTypeConfiguration : IEntityTypeConfiguration<Transfer>
    {
        public void Configure(EntityTypeBuilder<Transfer> builder)
        {
            builder.ToTable("Transfers").HasKey(x => x.Id);
        }
    }
}