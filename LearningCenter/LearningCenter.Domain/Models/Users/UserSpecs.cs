using FluentAssertions;
using LearningCenter.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LearningCenter.Domain.Models.Users
{
    public class UserSpecs
    {
        [Fact]
        public void User_Should_Be_Created_With_Valid_Name()
        {
            // Arrange
            string validName = "John Doe";

            // Act
            var user = new User(validName);

            // Assert
            user.Name.Should().Be(validName);
            user.Achievements.Should().BeEmpty();
            user.LessonsCompleted.Should().BeEmpty();
        }

        [Fact]
        public void User_Should_ThrowException_When_Name_Is_Invalid()
        {
            // Arrange
            string invalidName = ""; // Assuming MinNameLength > 0

            // Act
            Action act = () => new User(invalidName);

            // Assert
            act.Should()
                .Throw<InvalidUserException>()
                .WithMessage("*Name*");
        }

        [Fact]
        public void User_Should_Add_Achievement_Successfully()
        {
            // Arrange
            var user = new User("John Doe");

            // Act
            user.AddAchievement(1, false, 50);

            // Assert
            user.Achievements.Should().ContainSingle(a => a.AchievementId == 1);
        }

        [Fact]
        public void User_Should_ThrowException_When_Adding_Duplicate_Achievement()
        {
            // Arrange
            var user = new User("John Doe");
            user.AddAchievement(1, false, 50);

            // Act
            Action act = () => user.AddAchievement(1, false, 50);

            // Assert
            act.Should()
                .Throw<InvalidUserException>()
                .WithMessage("Achievement already exists for this user.");
        }

        [Fact]
        public void User_Should_Add_LessonCompleted_Successfully()
        {
            // Arrange
            var user = new User("John Doe");
            // Act
            user.AddLessonCompleted(1, DateTime.UtcNow.AddHours(-24), DateTime.UtcNow.AddHours(-20));
            user.AddLessonCompleted(2, DateTime.UtcNow.AddHours(1), DateTime.UtcNow.AddHours(2));

            // Assert
            user.LessonsCompleted.Should().HaveCount(2);
        }

        [Fact]
        public void User_Should_ThrowException_When_Adding_LessonCompletion_Not_In_Future()
        {
            // Arrange
            var user = new User("John Doe");
            user.AddLessonCompleted(1, DateTime.UtcNow.AddHours(-24), DateTime.UtcNow.AddHours(-20));
            user.AddLessonCompleted(2, DateTime.UtcNow.AddHours(1), DateTime.UtcNow.AddHours(2));
            user.AddLessonCompleted(3, DateTime.UtcNow.AddHours(1), DateTime.UtcNow.AddHours(2));

            // Act
            Action act = () => user.AddLessonCompleted(3, DateTime.UtcNow.AddHours(-1), DateTime.UtcNow);

            // Assert
            act.Should()
                .Throw<InvalidUserException>()
                .WithMessage("New lesson completion should be in the future.");
        }
    }
}
