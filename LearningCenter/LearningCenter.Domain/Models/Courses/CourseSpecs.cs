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
        public void AddChapter_ShouldAddChapter_WhenValidChapterProvided()
        {
            // Arrange
            var course = new Course("C# Fundamentals");

            // Act
            course.AddChapter("Introduction");

            // Assert
            course.Chapters.Should().ContainSingle();
            course.Chapters.First().Name.Should().Be("Introduction");
            course.Chapters.First().Order.Should().Be(1);
            course.Chapters.First().CourseId.Should().Be(course.Id);
        }

        [Fact]
        public void AddChapter_ShouldAddChaptersInOrder_WhenValidChaptersProvided()
        {
            // Arrange
            var course = new Course("C#");

            // Act
            course.AddChapter("Introduction");
            course.AddChapter("OOP");

            // Assert
            course.Chapters.Should().HaveCount(2);
            course.Chapters.First().Name.Should().Be("Introduction");
            course.Chapters.First().Order.Should().Be(1);
            course.Chapters.Last().Name.Should().Be("OOP");
            course.Chapters.Last().Order.Should().Be(2);
        }
    }
}
