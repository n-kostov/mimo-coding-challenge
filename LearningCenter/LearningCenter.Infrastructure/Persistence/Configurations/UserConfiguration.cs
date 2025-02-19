using LearningCenter.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static LearningCenter.Domain.Models.ModelConstants.Common;

namespace LearningCenter.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(MaxNameLength);

            builder.OwnsMany(u => u.Achievements, a =>
            {
                a.WithOwner();
                a.HasKey(p => p.Id);
                a.Property(p => p.AchievementId).IsRequired();
            });

            builder.OwnsMany(u => u.LessonsCompleted, lc =>
            {
                lc.WithOwner();
                lc.HasKey("Id");
                lc.Property(p => p.LessonId).IsRequired();
            });

            builder.ToTable("Users");
        }
    }
}
