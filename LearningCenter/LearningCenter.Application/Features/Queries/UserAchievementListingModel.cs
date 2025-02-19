namespace LearningCenter.Application.Features.Queries
{
    public class UserAchievementListingModel
    {
        public UserAchievementListingModel(int id, bool isCompleted, int progress) 
        {
            this.Id = id;
            this.IsCompleted = isCompleted;
            this.Progress = progress;
        }

        public int Id { get; }
        public bool IsCompleted { get; }
        public int Progress { get; }
    }
}
