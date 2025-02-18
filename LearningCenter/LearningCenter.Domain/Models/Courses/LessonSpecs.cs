using FluentAssertions;
using LearningCenter.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LearningCenter.Domain.Models.Courses
{
    public class LessonSpecs
    {
        [Fact]
        public void CreateLesson_ShouldSucceed_WhenValidParameters()
        {
            // Arrange
            string name = "Lesson 1";
            int chapterId = 1;
            int order = 5;

            // Act
            var lesson = new Lesson(name, order);

            // Assert
            lesson.Name.Should().Be(name);
            lesson.Order.Should().Be(order);
        }

        [Theory]
        [InlineData("")]
        [InlineData("A")] // Too short
        [InlineData("This lesson name is way too long to be valid because it exceeds the max length")] // Too long
        public void CreateLesson_ShouldThrowInvalidLessonException_WhenNameIsInvalid(string invalidName)
        {
            // Arrange
            int order = 5;

            // Act
            Action act = () => new Lesson(invalidName, order);

            // Assert
            act.Should().Throw<InvalidLessonException>();
        }

        [Theory]
        [InlineData(-1)] // Below min order
        [InlineData(1000)] // Above max order
        public void CreateLesson_ShouldThrowInvalidLessonException_WhenOrderIsOutOfRange(int invalidOrder)
        {
            // Arrange
            string name = "Valid Lesson Name";

            // Act
            Action act = () => new Lesson(name, invalidOrder);

            // Assert
            act.Should().Throw<InvalidLessonException>();
        }
    }
}
