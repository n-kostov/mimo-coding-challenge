using LearningCenter.Domain.Common;
using LearningCenter.Domain.Exceptions;
using static LearningCenter.Domain.Models.ModelConstants.Common;

namespace LearningCenter.Domain.Models.Courses
{
    public class Chapter : Entity<int>
    {
        public string Name { get; private set; }
        public int Order { get; private set; }
        private List<Lesson> _lessons = new();
        public IReadOnlyCollection<Lesson> Lessons => _lessons.AsReadOnly();
        private Chapter() 
        { 
        }

        internal Chapter(string name, int order) 
        {
            Validate(name, order);

            Name = name;
            Order = order; 
        }

        public void AddLesson(string name)
        {
            int nextOrder = _lessons.Count > 0 ? _lessons.Max(l => l.Order) + 1 : 1;
            var lesson = new Lesson(name, nextOrder);
            _lessons.Add(lesson);
        }

        private void Validate(string name, int order)
        {
            Guard.ForStringLength<InvalidChapterException>(
                name,
                MinNameLength,
                MaxNameLength,
                nameof(this.Name));

            Guard.AgainstOutOfRange<InvalidChapterException>(
                order,
                MinOrder,
                MaxOrder,
                nameof(this.Order));
        }
    }
}
