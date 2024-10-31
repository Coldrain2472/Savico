namespace Savico.Infrastructure.Data.DataSeeding.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Savico.Core.Models;

    public class GoalConfiguration : IEntityTypeConfiguration<Goal>
    {
        public void Configure(EntityTypeBuilder<Goal> builder)
        {
            builder.Property(p => p.CurrentAmount)
                .HasPrecision(18, 2);

            builder.Property(p=>p.TargetAmount)
                .HasPrecision(18, 2);
        }
    }
}
