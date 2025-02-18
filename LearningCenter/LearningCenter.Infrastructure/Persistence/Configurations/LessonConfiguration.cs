using LearningCenter.Domain.Models.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static LearningCenter.Domain.Models.ModelConstants.Common;

namespace LearningCenter.Infrastructure.Persistence.Configurations
{
    public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.Name)
                .IsRequired()
                .HasMaxLength(MaxNameLength);

            builder.Property(l => l.Order)
                .IsRequired();

            builder.ToTable("Lessons");
        }
    }

}
