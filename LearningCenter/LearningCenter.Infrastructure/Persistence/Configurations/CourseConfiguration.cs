using LearningCenter.Domain.Models.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static LearningCenter.Domain.Models.ModelConstants.Common;

namespace LearningCenter.Infrastructure.Persistence.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(MaxNameLength);

            builder.HasMany(c => c.Chapters)
                .WithOne()
                .HasForeignKey("CourseId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Courses");
        }
    }
}
