namespace LearningCenter.Application.Features.Queries
{
    public class GetUserAchievementsOutputModel
    {
        internal GetUserAchievementsOutputModel(IEnumerable<UserAchievementListingModel> userAchievements)
        {
            this.UserAchievements = userAchievements;
        }

        public IEnumerable<UserAchievementListingModel> UserAchievements { get; }
    }
}
