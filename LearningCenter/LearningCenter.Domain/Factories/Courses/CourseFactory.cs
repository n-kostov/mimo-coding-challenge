using LearningCenter.Domain.Exceptions;
using LearningCenter.Domain.Models.Courses;

namespace LearningCenter.Domain.Factories.Courses
{
    internal class CourseFactory : ICourseFactory
    {
        private string _name;
        private readonly List<Chapter> _chapters = new();

        public ICourseFactory WithName(string name)
        {
            _name = name;
            return this;
        }

        public ICourseFactory WithChapter(string name)
        {
            int nextOrder = _chapters.Count + 1;
            var chapter = new Chapter(name, nextOrder);
            _chapters.Add(chapter);
            return this;
        }

        public ICourseFactory WithLesson(string chapterName, string lessonName)
        {
            var chapter = _chapters.FirstOrDefault(c => c.Name == chapterName);
            if (chapter == null)
            {
                throw new InvalidCourseException("Chapter not found.");
            }

            chapter.AddLesson(lessonName);
            return this;
        }

        public Course Build()
        {
            var course = new Course(_name);
            foreach (var chapter in _chapters)
            {
                course.AddChapter(chapter);
            }

            _chapters.Clear();

            return course;
        }
    }
}
