using LearningCenter.Application.Features;
using LearningCenter.Domain.Models.Courses;
using Microsoft.EntityFrameworkCore;

namespace LearningCenter.Infrastructure.Persistence.Repositories
{
    internal class CourseRepository : DataRepository<Course>, ICourseRepository
    {
        public CourseRepository(LearningCenterDbContext db)
            : base(db)
        {
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await this.All()
                .Include(c => c.Chapters)
                    .ThenInclude(ch => ch.Lessons)
                .ToListAsync();
        }
    }
}
