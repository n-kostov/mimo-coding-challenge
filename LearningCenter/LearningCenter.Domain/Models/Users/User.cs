using LearningCenter.Domain.Common;
using LearningCenter.Domain.Exceptions;
using static LearningCenter.Domain.Models.ModelConstants.Common;

namespace LearningCenter.Domain.Models.Users
{
    public class User : Entity<int>, IAggregateRoot
    {
        public string Name { get; private set; }
        private List<UserAchievement> _achievements = new();
        private List<LessonCompleted> _lessonsCompleted = new();
        public IReadOnlyCollection<UserAchievement> Achievements => _achievements.AsReadOnly();
        public IReadOnlyCollection<LessonCompleted> LessonsCompleted => _lessonsCompleted.AsReadOnly();

        private User() 
        {
        }

        internal User(string name)
        {
            Validate(name);

            Name = name;
        }

        public void AddAchievement(int achievementId, bool isCompleted, int progress)
        {
            var achievement = new UserAchievement(achievementId, isCompleted, progress);
            if (_achievements.Any(a => a.AchievementId == achievement.AchievementId))
            {
                throw new InvalidUserException("Achievement already exists for this user.");
            }

            _achievements.Add(achievement);
        }

        public void AddLessonCompleted(int lessonId, DateTime startedOn, DateTime completedOn)
        {
            var lessonCompleted = new LessonCompleted(lessonId, startedOn, completedOn);
            if (_lessonsCompleted.Any(lc => lc.LessonId == lessonId && lc.CompletedOn > lessonCompleted.StartedOn))
            {
                throw new InvalidUserException("New lesson completion should be in the future.");
            }

            _lessonsCompleted.Add(lessonCompleted);
            AddDomainEvent(new LessonCompletedEvent(Id, lessonId, DateTime.UtcNow));
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
