using LearningCenter.Domain.Common;
using LearningCenter.Domain.Exceptions;
using static LearningCenter.Domain.Models.ModelConstants.UserAchievement;

namespace LearningCenter.Domain.Models.Users
{
    public class UserAchievement : ValueObject
    {
        public int AchievementId { get; private set; }
        public bool IsCompleted { get; private set; }
        public int Progress { get; private set; }

        private UserAchievement() { }

        internal UserAchievement(int achievementId, bool isCompleted, int progress)
        {
            Validate(progress);

            AchievementId = achievementId;
            IsCompleted = isCompleted;
            Progress = progress;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return AchievementId;
            yield return IsCompleted;
            yield return Progress;
        }

        private void Validate(int progress)
        {
            Guard.AgainstOutOfRange<InvalidUserAchievementException>(
                progress,
                MinProgress,
                MaxProgress,
                nameof(this.Progress));
        }
    }
}
