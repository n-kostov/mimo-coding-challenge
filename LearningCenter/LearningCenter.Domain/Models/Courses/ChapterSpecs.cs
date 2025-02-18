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
            var lesson = new Lesson("Lesson 1", chapter.Id, 1);

            // Act
            chapter.AddLesson(lesson);

            // Assert
            chapter.Lessons.Should().ContainSingle();
            chapter.Lessons.First().Should().Be(lesson);
        }

        [Fact]
        public void AddLesson_ShouldThrowException_WhenLessonOrderIsIncorrect()
        {
            // Arrange
            var chapter = new Chapter("Chapter 1", 1, 1);
            var lesson1 = new Lesson("Lesson 1", chapter.Id, 1);
            var lesson2 = new Lesson("Lesson 2", chapter.Id, 3);
            chapter.AddLesson(lesson1);

            // Act
            Action act = () => chapter.AddLesson(lesson2);

            // Assert
            act.Should().Throw<InvalidChapterException>().WithMessage("Lesson order must be 2.");
        }
    }
}
