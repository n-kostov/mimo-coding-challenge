using FluentAssertions;
using LearningCenter.Domain.Exceptions;
using LearningCenter.Domain.Models.Achievements;
using Xunit;

namespace LearningCenter.Domain.Factories.Achievements
{
    public class AchievementFactorySpecs
    {
        [Fact]
        public void BuildGeneral_ShouldCreateAchievement_WhenValidDataProvided()
        {
            // Arrange
            var factory = new AchievementFactory();

            // Act
            var achievement = factory.BuildGeneral("Complete 10 chapters", 10, AchievementUnitType.Chapter);

            // Assert
            achievement.Should().NotBeNull();
            achievement.Name.Should().Be("Complete 10 chapters");
            achievement.Goal.Should().Be(10);
            achievement.UnitType.Should().Be(AchievementUnitType.Chapter);
            achievement.TargetId.Should().BeNull();
        }

        [Fact]
        public void BuildSpecific_ShouldCreateAchievement_WhenValidDataProvided()
        {
            // Arrange
            var factory = new AchievementFactory();

            // Act
            var achievement = factory.BuildSpecific("Complete C# Course", AchievementUnitType.Course, 42);

            // Assert
            achievement.Should().NotBeNull();
            achievement.Name.Should().Be("Complete C# Course");
            achievement.Goal.Should().Be(1);
            achievement.UnitType.Should().Be(AchievementUnitType.Course);
            achievement.TargetId.Should().Be(42);
        }

        [Fact]
        public void Build_ShouldThrowException_WhenNameIsNotProvided()
        {
            // Arrange
            var factory = new AchievementFactory();

            // Act
            Action act = () => factory.BuildGeneral("", 5, AchievementUnitType.Chapter);

            // Assert
            act.Should().Throw<InvalidAchievementException>()
                .WithMessage("*Name*");
        }

        [Fact]
        public void Build_ShouldThrowException_WhenGoalIsOutOfRange()
        {
            // Arrange
            var factory = new AchievementFactory();

            // Act
            Action act = () => factory.BuildGeneral("Complete 10 chapters", -1, AchievementUnitType.Chapter);

            // Assert
            act.Should().Throw<InvalidAchievementException>()
                .WithMessage("*Goal*");
        }

        [Fact]
        public void Build_ShouldThrowException_WhenTargetIdIsInvalid()
        {
            // Arrange
            var factory = new AchievementFactory();

            // Act
            Action act = () => factory.BuildSpecific("Complete C# Course", AchievementUnitType.Course, -5);

            // Assert
            act.Should().Throw<InvalidAchievementException>()
                .WithMessage("*TargetId*");
        }
    }
}
