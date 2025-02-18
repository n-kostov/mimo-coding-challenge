using FluentAssertions;
using LearningCenter.Domain.Exceptions;
using Xunit;

namespace LearningCenter.Domain.Models.Courses
{
    public class CourseSpecs
    {
        [Fact]
        public void Course_Should_Be_Created_With_Valid_Name()
        {
            // Arrange
            string validName = "C# Fundamentals";

            // Act
            var course = new Course(validName);

            // Assert
            course.Name.Should().Be(validName);
            course.Chapters.Should().BeEmpty();
        }

        [Fact]
        public void AddChapter_Should_Add_Chapter_With_Correct_Order()
        {
            // Arrange
            var course = new Course("C# Fundamentals");
            var chapter1 = new Chapter("Introduction", 1, 1);
            var chapter2 = new Chapter("OOP Basics", 1, 2);

            // Act
            course.AddChapter(chapter1);
            course.AddChapter(chapter2);

            // Assert
            course.Chapters.Should().HaveCount(2);
            course.Chapters.First().Should().Be(chapter1);
            course.Chapters.Last().Should().Be(chapter2);
        }

        [Fact]
        public void AddChapter_Should_ThrowException_When_Order_Is_Wrong()
        {
            // Arrange
            var course = new Course("C# Fundamentals");
            var chapter1 = new Chapter("Introduction", 1, 1);
            var chapter2 = new Chapter("OOP Basics", 1, 3); // Wrong order (should be 2)

            course.AddChapter(chapter1);

            // Act
            Action act = () => course.AddChapter(chapter2);

            // Assert
            act.Should()
                .Throw<InvalidCourseException>()
                .WithMessage("Chapter order must be 2.");
        }
    }
}
