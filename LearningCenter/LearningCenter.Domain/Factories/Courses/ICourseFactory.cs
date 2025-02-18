using LearningCenter.Domain.Models.Courses;

namespace LearningCenter.Domain.Factories.Courses
{
    public interface ICourseFactory : IFactory<Course>
    {
        ICourseFactory WithName(string name);
        ICourseFactory WithChapter(string name);
        ICourseFactory WithLesson(string chapterName, string lessonName);
    }
}
