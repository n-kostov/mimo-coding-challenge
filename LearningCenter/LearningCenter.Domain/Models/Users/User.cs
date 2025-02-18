using LearningCenter.Domain.Common;
using LearningCenter.Domain.Exceptions;
using static LearningCenter.Domain.Models.ModelConstants.Common;

namespace LearningCenter.Domain.Models.Users
{
    public class User : Entity<int>, IAggregateRoot
    {
        public string Name { get; private set; }
        public List<UserAchievement> Achievements { get; private set; } = new();
        public List<LessonCompleted> LessonsCompleted { get; private set; } = new();

        private User() 
        {
        }

        public User(string name)
        {
            Validate(name);

            Name = name;
        }

        public void AddAchievement(int achievementId, bool isCompleted, int progress)
        {
            var achievement = new UserAchievement(achievementId, isCompleted, progress);
            if (Achievements.Any(a => a.AchievementId == achievement.AchievementId))
            {
                throw new InvalidUserException("Achievement already exists for this user.");
            }

            Achievements.Add(achievement);
        }

        public void AddLessonCompleted(int lessonId, DateTime startedOn, DateTime completedOn)
        {
            var lessonCompleted = new LessonCompleted(lessonId, startedOn, completedOn);
            if (LessonsCompleted.Any(lc => lc.CompletedOn > lessonCompleted.StartedOn))
            {
                throw new InvalidUserException("New lesson completion should be in the future.");
            }

            LessonsCompleted.Add(lessonCompleted);
        }

        private void Validate(string name)
        {
            Guard.ForStringLength<InvalidUserException>(
                name,
                MinNameLength,
                MaxNameLength,
                nameof(this.Name));
        }
    }
}
