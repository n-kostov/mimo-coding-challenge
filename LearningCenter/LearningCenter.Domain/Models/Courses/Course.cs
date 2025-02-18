using LearningCenter.Domain.Common;
using LearningCenter.Domain.Exceptions;
using System.Xml.Linq;
using static LearningCenter.Domain.Models.ModelConstants.Common;

namespace LearningCenter.Domain.Models.Courses
{
    public class Course : Entity<int>, IAggregateRoot
    {
        public string Name { get; private set; }
        private List<Chapter> _chapters = new();
        public IReadOnlyCollection<Chapter> Chapters => _chapters.AsReadOnly();
        private Course()
        { 
        }

        internal Course(string name) 
        {
            Validate(name);

            Name = name; 
        }

        public void AddChapter(string name)
        {
            int nextOrder = _chapters.Count > 0 ? _chapters.Max(c => c.Order) + 1 : 1;
            var chapter = new Chapter(name, nextOrder);
            _chapters.Add(chapter);
        }

        public void AddLessonToChapter(int chapterId, string lessonName)
        {
            var chapter = _chapters.FirstOrDefault(c => c.Id == chapterId);
            if (chapter == null)
            {
                throw new InvalidCourseException("Chapter not found.");
            }

            chapter.AddLesson(lessonName);
        }

        private void Validate(string name)
        {
            Guard.ForStringLength<InvalidCourseException>(
                name,
                MinNameLength,
                MaxNameLength,
                nameof(this.Name));
        }
    }
}
