using FluentAssertions;
using LearningCenter.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LearningCenter.Domain.Models.Achievements
{
    public class AchievementSpecs
    {
        [Fact]
        public void GeneralAchievement_Should_Be_Created_With_Valid_Parameters()
        {
            // Arrange
            string validName = "Complete 5 chapters";
            int validGoal = 5;
            var unitType = AchievementUnitType.Chapter;

            // Act
            var achievement = new Achievement(validName, validGoal, unitType);

            // Assert
            achievement.Name.Should().Be(validName);
            achievement.Goal.Should().Be(validGoal);
            achievement.UnitType.Should().Be(unitType);
            achievement.TargetId.Should().BeNull();
        }

        [Fact]
        public void SpecificAchievement_Should_Be_Created_With_Valid_Parameters()
        {
            // Arrange
            string validName = "Complete C# Course";
            var unitType = AchievementUnitType.Course;
            int targetId = 1;

            // Act
            var achievement = new Achievement(validName, unitType, targetId);

            // Assert
            achievement.Name.Should().Be(validName);
            achievement.Goal.Should().Be(1);
            achievement.UnitType.Should().Be(unitType);
            achievement.TargetId.Should().Be(targetId);
        }

        [Fact]
        public void GeneralAchievement_Should_ThrowException_When_Name_Is_Invalid()
        {
            // Arrange
            string invalidName = ""; // Empty name
            int validGoal = 5;
            var unitType = AchievementUnitType.Course;

            // Act
            Action act = () => new Achievement(invalidName, validGoal, unitType);

            // Assert
            act.Should()
                .Throw<InvalidAchievementException>()
                .WithMessage("*Name*");
        }

        [Fact]
        public void GeneralAchievement_Should_ThrowException_When_Goal_Is_Invalid()
        {
            // Arrange
            string validName = "Complete 5 chapters";
            int invalidGoal = -1; // Invalid goal
            var unitType = AchievementUnitType.Chapter;

            // Act
            Action act = () => new Achievement(validName, invalidGoal, unitType);

            // Assert
            act.Should()
                .Throw<InvalidAchievementException>()
                .WithMessage("*Goal*");
        }

        [Fact]
        public void SpecificAchievement_Should_ThrowException_When_Name_Is_Invalid()
        {
            // Arrange
            string invalidName = ""; // Empty name
            var unitType = AchievementUnitType.Course;
            int targetId = 1;

            // Act
            Action act = () => new Achievement(invalidName, unitType, targetId);

            // Assert
            act.Should()
                .Throw<InvalidAchievementException>()
                .WithMessage("*Name*");
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void SpecificAchievement_Should_ThrowException_When_TargetId_Is_Invalid(int targetId)
        {
            // Arrange
            string validName = "Complete C# Course";
            var unitType = AchievementUnitType.Course;

            // Act
            Action act = () => new Achievement(validName, unitType, targetId);

            // Assert
            act.Should()
                .Throw<InvalidAchievementException>()
                .WithMessage("*TargetId*");
        }
    }
}
