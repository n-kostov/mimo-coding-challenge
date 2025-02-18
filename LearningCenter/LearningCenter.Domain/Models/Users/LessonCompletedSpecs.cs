using FluentAssertions;
using LearningCenter.Domain.Exceptions;
using Xunit;

namespace LearningCenter.Domain.Models.Users
{
    public class LessonCompletedSpecs
    {
        [Fact]
        public void LessonCompleted_Should_Be_Created_With_Valid_Dates()
        {
            // Arrange
            int lessonId = 1;
            DateTime startedOn = DateTime.UtcNow.AddHours(-1);
            DateTime completedOn = DateTime.UtcNow;

            // Act
            var lessonCompleted = new LessonCompleted(lessonId, startedOn, completedOn);

            // Assert
            lessonCompleted.LessonId.Should().Be(lessonId);
            lessonCompleted.StartedOn.Should().Be(startedOn);
            lessonCompleted.CompletedOn.Should().Be(completedOn);
        }

        [Fact]
        public void LessonCompleted_Should_ThrowException_When_CompletedOn_Before_StartedOn()
        {
            // Arrange
            int lessonId = 1;
            DateTime startedOn = DateTime.UtcNow;
            DateTime completedOn = startedOn.AddHours(-1); // Invalid case

            // Act
            Action act = () => new LessonCompleted(lessonId, startedOn, completedOn);

            // Assert
            act.Should()
                .Throw<InvalidLessonException>()
                .WithMessage("*CompletedOn*");
        }
    }
}
