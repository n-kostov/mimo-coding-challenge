using LearningCenter.Domain.Common;
using LearningCenter.Domain.Models.Achievements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static LearningCenter.Domain.Models.ModelConstants.Common;

namespace LearningCenter.Infrastructure.Persistence.Configurations
{
    public class AchievementConfiguration : IEntityTypeConfiguration<Achievement>
    {
        public void Configure(EntityTypeBuilder<Achievement> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(MaxNameLength);

            builder.Property(a => a.Goal)
                .IsRequired();

            builder.Property(a => a.UnitType)
                .HasConversion(
                    v => v.Name,  // Convert to string when saving
                    v => Enumeration.FromName<AchievementUnitType>(v) // Convert back when reading
                )
                .IsRequired();

            builder.Property(a => a.TargetId)
                .IsRequired(false);

            builder.ToTable("Achievements");
        }
    }
}
