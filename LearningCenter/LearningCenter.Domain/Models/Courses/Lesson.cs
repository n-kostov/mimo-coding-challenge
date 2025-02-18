using LearningCenter.Domain.Common;
using LearningCenter.Domain.Exceptions;
using static LearningCenter.Domain.Models.ModelConstants.Common;

namespace LearningCenter.Domain.Models.Courses
{
    public class Lesson : Entity<int>
    {
        public string Name { get; private set; }
        public int ChapterId { get; private set; }
        public int Order { get; private set; }
        private Lesson() 
        { 
        }

        internal Lesson(string name, int chapterId, int order) 
        {
            Validate(name, order);

            Name = name;
            ChapterId = chapterId;
            Order = order; 
        }

        private void Validate(string name, int order)
        {
            Guard.ForStringLength<InvalidLessonException>(
                name,
                MinNameLength,
                MaxNameLength,
                nameof(this.Name));

            Guard.AgainstOutOfRange<InvalidLessonException>(
                order,
                MinOrder,
                MaxOrder,
                nameof(this.Order));
        }
    }
}
