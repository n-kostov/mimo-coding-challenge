﻿using LearningCenter.Domain.Common;
using LearningCenter.Domain.Exceptions;
using System.Xml.Linq;
using static LearningCenter.Domain.Models.ModelConstants.Common;

namespace LearningCenter.Domain.Models.Courses
{
    public class Chapter : Entity<int>
    {
        public string Name { get; private set; }
        public int CourseId { get; private set; }
        public int Order { get; private set; }
        private List<Lesson> _lessons = new();
        public IReadOnlyCollection<Lesson> Lessons => _lessons.AsReadOnly();
        private Chapter() 
        { 
        }

        internal Chapter(string name, int courseId, int order) 
        {
            Validate(name, order);

            Name = name;
            CourseId = courseId; 
            Order = order; 
        }

        public void AddLesson(Lesson lesson)
        {
            int nextOrder = _lessons.Count > 0 ? _lessons.Max(l => l.Order) + 1 : 1;
            if (lesson.Order != nextOrder)
            {
                throw new InvalidLessonException($"Lesson order must be {nextOrder}.");
            }
            _lessons.Add(lesson);
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
