using LearningCenter.Domain.Common;
using LearningCenter.Domain.Exceptions;
using static LearningCenter.Domain.Models.ModelConstants.UserAchievement;

namespace LearningCenter.Domain.Models.Users
{
    public class UserAchievement : Entity<int>
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

        public void UpdateProgress(bool isCompleted)
        {
            int newProgress = Progress + 1;
            Validate(newProgress);

            IsCompleted = isCompleted;
            Progress = newProgress;
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
