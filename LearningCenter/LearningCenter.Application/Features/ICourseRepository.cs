using LearningCenter.Application.Contracts;
using LearningCenter.Domain.Models.Courses;

namespace LearningCenter.Application.Features
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<IEnumerable<Course>> GetAllAsync();

        Task<bool> DoesLessonExistAsync(int lessonId);
    }
}
