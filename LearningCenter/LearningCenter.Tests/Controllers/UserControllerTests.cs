using FluentAssertions;
using LearningCenter.Application.Features.Commands;
using LearningCenter.Application.Features.Queries;
using LearningCenter.Tests.Common;
using System.Text;
using System.Text.Json;

namespace LearningCenter.Tests.Controllers
{
    public class UserControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public UserControllerTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        private async Task CompleteLessonAsync(int userId, int lessonId)
        {
            var command = new CompleteLessonCommand(userId, lessonId, new DateTime(2025, 2, 15), new DateTime(2025, 2, 16));
            var content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/user/complete-lesson", content);
            response.EnsureSuccessStatusCode();
        }

        private async Task<GetUserAchievementsOutputModel> GetUserAchievementsAsync(int userId)
        {
            var response = await _client.GetAsync($"/api/user/achievements?UserId={userId}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GetUserAchievementsOutputModel>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        }

        [Fact]
        public async Task CompleteLessons_ShouldAccumulateAchievements()
        {
            int userId = 1;

            var achievements = await GetUserAchievementsAsync(userId);

            achievements.Should().NotBeNull();
            achievements.UserAchievements.Should().NotContain(a => a.Id == 1); // Complete 5 lessons
            achievements.UserAchievements.Should().NotContain(a => a.Id == 4); // Complete 1 chapter

            await CompleteLessonAsync(userId, 1); // Variables
            await CompleteLessonAsync(userId, 2); // Data Types

            achievements = await GetUserAchievementsAsync(userId);

            achievements.Should().NotBeNull();
            achievements.UserAchievements.Should().Contain(a => a.Id == 1 && !a.IsCompleted && a.Progress == 2); // Complete 5 lessons
            achievements.UserAchievements.Should().Contain(a => a.Id == 4 && a.IsCompleted && a.Progress == 1); // Complete 1 chapter

            await CompleteLessonAsync(userId, 3); // What is swift
            await CompleteLessonAsync(userId, 4); // Hello world
            await CompleteLessonAsync(userId, 5); // Variables (Swift)

            achievements = await GetUserAchievementsAsync(userId);

            achievements.Should().NotBeNull();
            achievements.UserAchievements.Should().Contain(a => a.Id == 1 && a.IsCompleted && a.Progress == 5); // Complete 5 lessons
            achievements.UserAchievements.Should().Contain(a => a.Id == 4 && a.IsCompleted && a.Progress == 1); // Complete 1 chapter
        }

        [Fact]
        public async Task CompleteCourse_ShouldGrantCourseAchievement()
        {
            int userId = 1;

            var achievements = await GetUserAchievementsAsync(userId);

            achievements.Should().NotBeNull();
            achievements.UserAchievements.Should().NotContain(a => a.Id == 6); // Complete the Swift course
            achievements.UserAchievements.Should().NotContain(a => a.Id == 7); // Complete the C# course

            await CompleteLessonAsync(userId, 1);
            await CompleteLessonAsync(userId, 2);
            await CompleteLessonAsync(userId, 3);
            await CompleteLessonAsync(userId, 4);
            await CompleteLessonAsync(userId, 5);

            achievements = await GetUserAchievementsAsync(userId);

            achievements.Should().NotBeNull();
            achievements.UserAchievements.Should().NotContain(a => a.Id == 6); // Complete the Swift course
            achievements.UserAchievements.Should().Contain(a => a.Id == 7 && a.IsCompleted); // Complete the C# course

            await CompleteLessonAsync(userId, 6);

            achievements = await GetUserAchievementsAsync(userId);
            achievements.UserAchievements.Should().Contain(a => a.Id == 6 && a.IsCompleted); // Complete the Swift course
            achievements.UserAchievements.Should().Contain(a => a.Id == 7 && a.IsCompleted); // Complete the C# course
        }
    }
}
