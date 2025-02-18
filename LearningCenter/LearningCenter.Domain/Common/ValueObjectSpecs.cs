using FluentAssertions;
using LearningCenter.Domain.Models.Users;
using Xunit;

namespace LearningCenter.Domain.Common
{
    public class ValueObjectSpecs
    {
        [Fact]
        public void ValueObjectsWithEqualPropertiesShouldBeEqual()
        {
            DateTime startedOn = new DateTime(2021, 1, 1);
            DateTime completedOn = new DateTime(2021, 1, 2);
            // Arrange
            var first = new LessonCompleted(1, startedOn, completedOn);
            var second = new LessonCompleted(1, startedOn, completedOn);

            // Act
            var result = first == second;

            // Arrange
            result.Should().BeTrue();
        }

        [Fact]
        public void ValueObjectsWithDifferentPropertiesShouldNotBeEqual()
        {
            DateTime startedOn = new DateTime(2021, 1, 1);
            DateTime completedOn = new DateTime(2021, 1, 2);
            // Arrange
            var first = new LessonCompleted(1, startedOn, completedOn);
            var second = new LessonCompleted(1, startedOn, completedOn.AddDays(1));

            // Act
            var result = first == second;

            // Arrange
            result.Should().BeFalse();
        }
    }
}
