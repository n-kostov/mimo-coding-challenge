using LearningCenter.Domain.Common;

namespace LearningCenter.Domain.Models.Achievements
{
    public class AchievementUnitType : Enumeration
    {
        public static readonly AchievementUnitType Course = new(1, nameof(Course));
        public static readonly AchievementUnitType Chapter = new(2, nameof(Chapter));
        public static readonly AchievementUnitType Lesson = new(3, nameof(Lesson));

        private AchievementUnitType(int value)
            : this(value, FromValue<AchievementUnitType>(value).Name)
        {
        }

        private AchievementUnitType(int value, string name)
            : base(value, name)
        {
        }
    }
}
