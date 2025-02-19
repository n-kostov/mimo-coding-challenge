using LearningCenter.Application.Contracts;
using LearningCenter.Domain.Models.Courses;

namespace LearningCenter.Application.Features
{
    public interface ICourseRepository : IRepository<Course>
    {
        IEnumerable<Course> GetAll();
    }
}
