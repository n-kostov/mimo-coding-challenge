using LearningCenter.Domain.Models.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static LearningCenter.Domain.Models.ModelConstants.Common;

namespace LearningCenter.Infrastructure.Persistence.Configurations
{
    public class ChapterConfiguration : IEntityTypeConfiguration<Chapter>
    {
        public void Configure(EntityTypeBuilder<Chapter> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(MaxNameLength);

            builder.Property(c => c.Order)
                .IsRequired();

            builder.HasMany(c => c.Lessons)
                .WithOne()
                .HasForeignKey("ChapterId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Chapters");
        }
    }
}
