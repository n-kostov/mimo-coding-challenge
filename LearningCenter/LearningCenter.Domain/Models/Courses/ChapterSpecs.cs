using FluentAssertions;
using LearningCenter.Domain.Exceptions;
using Xunit;

namespace LearningCenter.Domain.Models.Courses
{
    public class ChapterSpecs
    {
        [Fact]
        public void Constructor_ShouldCreateChapter_WhenValidDataProvided()
        {
            // Arrange
            var name = "Introduction";
            var courseId = 1;
            var order = 1;

            // Act
            var chapter = new Chapter(name, courseId, order);

            // Assert
            chapter.Name.Should().Be(name);
            chapter.CourseId.Should().Be(courseId);
            chapter.Order.Should().Be(order);
            chapter.Lessons.Should().BeEmpty();
        }

        [Theory]
        [InlineData("")]
        [InlineData("A")]
        [InlineData("This name is way too long for a valid chapter name beyond limits")]
        public void Constructor_ShouldThrowException_WhenInvalidNameProvided(string invalidName)
        {
            // Act
            Action act = () => new Chapter(invalidName, 1, 1);

            // Assert
            act.Should().Throw<InvalidChapterException>();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1000)]
        public void Constructor_ShouldThrowException_WhenOrderIsOutOfRange(int invalidOrder)
        {
            // Act
            Action act = () => new Chapter("Valid Name", 1, invalidOrder);

            // Assert
            act.Should().Throw<InvalidChapterException>();
        }

        [Fact]
        public void AddLesson_ShouldAddLesson_WhenValidLessonProvided()
        {
            // Arrange
            var chapter = new Chapter("Chapter 1", 1, 1);

            // Act
            chapter.AddLesson("Lesson 1");

            // Assert
            chapter.Lessons.Should().ContainSingle();
            chapter.Lessons.First().Name.Should().Be("Lesson 1");
            chapter.Lessons.First().Order.Should().Be(1);
            chapter.Lessons.First().ChapterId.Should().Be(chapter.Id);
        }

        [Fact]
        public void AddLesson_ShouldAddLessonsInOrder_WhenValidLessonsProvided()
        {
            // Arrange
            var chapter = new Chapter("Chapter 1", 1, 1);

            // Act
            chapter.AddLesson("Lesson 1");
            chapter.AddLesson("Lesson 2");

            // Assert
            chapter.Lessons.Should().HaveCount(2);
            chapter.Lessons.First().Name.Should().Be("Lesson 1");
            chapter.Lessons.First().Order.Should().Be(1);
            chapter.Lessons.Last().Name.Should().Be("Lesson 2");
            chapter.Lessons.Last().Order.Should().Be(2);
        }
    }
}
