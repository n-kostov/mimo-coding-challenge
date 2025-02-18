using FluentAssertions;
using LearningCenter.Domain.Exceptions;
using Xunit;

namespace LearningCenter.Domain.Factories.Courses
{
    public class CourseFactorySpecs
    {
        [Fact]
        public void Build_ShouldCreateCourse_WhenValidNameProvided()
        {
            // Arrange
            var factory = new CourseFactory();

            // Act
            var course = factory.WithName("Math 101").Build();

            // Assert
            course.Should().NotBeNull();
            course.Name.Should().Be("Math 101");
            course.Chapters.Should().BeEmpty();
        }

        [Fact]
        public void Build_ShouldCreateCourseWithChapters_WhenChaptersAreAdded()
        {
            // Arrange
            var factory = new CourseFactory();

            // Act
            var course = factory
                .WithName("Science 101")
                .WithChapter("Physics")
                .WithChapter("Chemistry")
                .Build();

            // Assert
            course.Should().NotBeNull();
            course.Name.Should().Be("Science 101");
            course.Chapters.Should().HaveCount(2);
            course.Chapters.Select(c => c.Name).Should().Contain(new[] { "Physics", "Chemistry" });
        }

        [Fact]
        public void WithLesson_ShouldAddLessonToExistingChapter()
        {
            // Arrange
            var course = new CourseFactory()
                .WithName("History 101")
                .WithChapter("Ancient Civilizations")
                .WithLesson("Ancient Civilizations", "Egyptian History")
                .Build();

            // Assert
            course.Chapters.Should().ContainSingle();
            course.Chapters.First().Lessons.Should().ContainSingle(l => l.Name == "Egyptian History");
        }

        [Fact]
        public void WithLesson_ShouldThrowException_WhenChapterDoesNotExist()
        {
            // Arrange
            var factory = new CourseFactory().WithName("Programming 101");

            // Act
            Action act = () => factory.WithLesson("Ancient Civilizations", "C# Basics");

            // Assert
            act.Should().Throw<InvalidCourseException>()
                .WithMessage("Chapter not found.");
        }

        [Fact]
        public void Build_ShouldThrowException_WhenNameIsNotProvided()
        {
            // Arrange
            var factory = new CourseFactory();

            // Act
            Action act = () => factory.Build();

            // Assert
            act.Should().Throw<InvalidCourseException>()
                .WithMessage("*Name*");
        }
    }
}
