using LearningCenter.Domain.Common;
using LearningCenter.Domain.Exceptions;
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

        public Course(string name) 
        {
            Validate(name);

            Name = name; 
        }

        public void AddChapter(Chapter chapter)
        {
            int nextOrder = _chapters.Count > 0 ? _chapters.Max(c => c.Order) + 1 : 1;
            if (chapter.Order != nextOrder)
            {
                throw new InvalidCourseException($"Chapter order must be {nextOrder}.");
            }
            _chapters.Add(chapter);
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
