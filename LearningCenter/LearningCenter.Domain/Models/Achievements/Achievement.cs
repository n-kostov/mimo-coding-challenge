using LearningCenter.Domain.Common;
using LearningCenter.Domain.Exceptions;
using static LearningCenter.Domain.Models.ModelConstants.Common;
using static LearningCenter.Domain.Models.ModelConstants.Achievement;

namespace LearningCenter.Domain.Models.Achievements
{
    public class Achievement : Entity<int>, IAggregateRoot
    {
        public string Name { get; private set; }
        public int Goal { get; private set; }
        public AchievementUnitType UnitType { get; private set; }
        public int? TargetId { get; private set; }

        private Achievement() { }

        // General Achievement Constructor
        public Achievement(string name, int goal, AchievementUnitType unitType)
        {
            ValidateGeneralAchievement(name, goal);

            Name = name;
            Goal = goal;
            UnitType = unitType;
            TargetId = null;
        }

        // Specific Achievement Constructor
        public Achievement(string name, AchievementUnitType unitType, int targetId)
        {
            ValidateSpecificAchievement(name, targetId);

            Goal = 1;
            Name = name;
            UnitType = unitType;
            TargetId = targetId;
        }

        private void ValidateGeneralAchievement(string name, int goal)
        {
            Guard.ForStringLength<InvalidAchievementException>(
                name,
                MinNameLength,
                MaxNameLength,
                nameof(this.Name));

            Guard.AgainstOutOfRange<InvalidAchievementException>(
                goal,
                MinGoal,
                MaxGoal,
                nameof(this.Name));
        }

        private void ValidateSpecificAchievement(string name, int targetId)
        {
            Guard.ForStringLength<InvalidAchievementException>(
                name,
                MinNameLength,
                MaxNameLength,
                nameof(this.Name));

            Guard.AgainstGreaterThanOrEqual<InvalidAchievementException>
                (targetId,
                MinTargetId,
                nameof(this.TargetId));
        }
    }
}
