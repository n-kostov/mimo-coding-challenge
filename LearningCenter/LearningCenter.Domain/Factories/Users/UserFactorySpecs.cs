using FluentAssertions;
using LearningCenter.Domain.Exceptions;
using Xunit;

namespace LearningCenter.Domain.Factories.Users
{
    public class UserFactorySpecs
    {
        [Fact]
        public void Build_ShouldCreateUser_WhenNameIsValid()
        {
            // Arrange
            var factory = new UserFactory();

            // Act
            var user = factory.WithName("John Doe").Build();

            // Assert
            user.Should().NotBeNull();
            user.Name.Should().Be("John Doe");
        }

        [Fact]
        public void Build_ShouldThrowException_WhenNameIsNotProvided()
        {
            // Arrange
            var factory = new UserFactory();

            // Act
            Action act = () => factory.Build();

            // Assert
            act.Should().Throw<InvalidUserException>()
                .WithMessage("*Name*"); // Ensure error is related to Name
        }

        [Fact]
        public void Build_ShouldThrowException_WhenNameIsTooShort()
        {
            // Arrange
            var factory = new UserFactory();

            // Act
            Action act = () => factory.WithName("J").Build();

            // Assert
            act.Should().Throw<InvalidUserException>()
                .WithMessage("*Name*");
        }

        [Fact]
        public void Build_ShouldThrowException_WhenNameIsTooLong()
        {
            // Arrange
            var longName = new string('A', 51);
            var factory = new UserFactory();

            // Act
            Action act = () => factory.WithName(longName).Build();

            // Assert
            act.Should().Throw<InvalidUserException>()
                .WithMessage("*Name*");
        }
    }
}
