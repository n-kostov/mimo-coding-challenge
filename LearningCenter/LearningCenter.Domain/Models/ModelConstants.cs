namespace LearningCenter.Domain.Models
{
    public class ModelConstants
    {
        public class Common
        {
            public const int MinNameLength = 3;
            public const int MaxNameLength = 50;
            public const int MinOrder = 1;
            public const int MaxOrder = 100;
        }

        public class Achievement
        {
            public const int MinGoal = 1;
            public const int MaxGoal = 100;
            public const int MinTargetId = 1;
        }

        public class UserAchievement
        {
            public const int MinProgress = 1;
            public const int MaxProgress = 100;
        }
    }
}
