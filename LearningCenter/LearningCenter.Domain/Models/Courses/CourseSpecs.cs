using FluentAssertions;
using LearningCenter.Domain.Exceptions;
using Xunit;

namespace LearningCenter.Domain.Models.Courses
{
    public class CourseSpecs
    {
        [Fact]
        public void Constructor_ShouldCreateCourse_WhenNameIsValid()
        {
            // Arrange
            var courseName = "C# Fundamentals";

            // Act
            var course = new Course(courseName);

            // Assert
            course.Name.Should().Be(courseName);
            course.Chapters.Should().BeEmpty();
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenNameIsInvalid()
        {
            // Arrange
            var invalidName = "";

            // Act
            Action act = () => new Course(invalidName);

            // Assert
            act.Should().Throw<InvalidCourseException>();
        }

        [Fact]
        public void AddChapter_ShouldAddChapterWithCorrectOrder()
        {
            // Arrange
            var course = new Course("C# Fundamentals");

            // Act
            course.AddChapter("Introduction");
            course.AddChapter("Advanced Topics");

            // Assert
            course.Chapters.Should().HaveCount(2);
            course.Chapters.ElementAt(0).Order.Should().Be(1);
            course.Chapters.ElementAt(1).Order.Should().Be(2);
        }

        [Fact]
        public void AddLessonToChapter_ShouldAddLessonToExistingChapter()
        {
            // Arrange
            var course = new Course("C# Fundamentals");
            course.AddChapter("Introduction");
            var chapterId = course.Chapters.First().Id;

            // Act
            course.AddLessonToChapter(chapterId, "Hello World");

            // Assert
            var chapter = course.Chapters.First();
            chapter.Lessons.Should().HaveCount(1);
            chapter.Lessons.First().Name.Should().Be("Hello World");
        }

        [Fact]
        public void AddLessonToChapter_ShouldThrowException_WhenChapterDoesNotExist()
        {
            // Arrange
            var course = new Course("C# Fundamentals");
            var nonExistentChapterId = 999;

            // Act
            Action act = () => course.AddLessonToChapter(nonExistentChapterId, "Hello World");

            // Assert
            act.Should().Throw<InvalidCourseException>().WithMessage("Chapter not found.");
        }
    }
}
