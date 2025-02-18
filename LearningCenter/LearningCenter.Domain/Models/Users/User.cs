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

        public void AddAchievement(UserAchievement achievement)
        {
            if (Achievements.Any(a => a.AchievementId == achievement.AchievementId))
            {
                throw new InvalidUserException("Achievement already exists for this user.");
            }

            Achievements.Add(achievement);
        }

        public void AddLessonCompleted(LessonCompleted lessonCompleted)
        {
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
