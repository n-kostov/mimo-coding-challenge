using System.Text.Json.Serialization;

namespace LearningCenter.Application.Features.Queries
{
    public class GetUserAchievementsOutputModel
    {
        [JsonConstructor]
        internal GetUserAchievementsOutputModel(IEnumerable<UserAchievementListingModel> userAchievements)
        {
            this.UserAchievements = userAchievements;
        }

        public IEnumerable<UserAchievementListingModel> UserAchievements { get; }
    }
}
