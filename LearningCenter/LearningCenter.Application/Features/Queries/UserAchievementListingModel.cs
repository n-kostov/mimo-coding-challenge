namespace LearningCenter.Application.Features.Queries
{
    public class UserAchievementListingModel
    {
        public UserAchievementListingModel(int id, string name, bool isCompleted, int goal, int progress) 
        {
            this.Id = id;
            this.Name = name;
            this.IsCompleted = isCompleted;
            this.Goal = goal;
            this.Progress = progress;
        }

        public int Id { get; }
        public string Name { get; }
        public bool IsCompleted { get; }
        public int Goal { get; }
        public int Progress { get; }
    }
}
