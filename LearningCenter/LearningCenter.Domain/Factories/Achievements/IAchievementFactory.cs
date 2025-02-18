using LearningCenter.Domain.Models.Achievements;

namespace LearningCenter.Domain.Factories.Achievements
{
    public interface IAchievementFactory : IFactory<Achievement>
    {
        IAchievementFactory WithName(string name);

        IAchievementFactory WithGoal(int goal);

        IAchievementFactory WithUnitType(AchievementUnitType unitType);

        IAchievementFactory WithTarget(int targetId);
    }
}
