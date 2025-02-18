using LearningCenter.Domain.Models.Achievements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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

            var unitTypeConverter = new ValueConverter<AchievementUnitType, string>(
                v => v.ToString(),
                v => (AchievementUnitType)Enum.Parse(typeof(AchievementUnitType), v)
            );

            builder.Property(a => a.UnitType)
                .HasConversion(unitTypeConverter)
                .IsRequired();

            builder.Property(a => a.TargetId)
                .IsRequired(false);

            builder.ToTable("Achievements");
        }
    }
}
