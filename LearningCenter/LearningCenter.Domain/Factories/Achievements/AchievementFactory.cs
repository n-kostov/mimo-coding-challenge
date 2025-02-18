using LearningCenter.Domain.Models.Achievements;

namespace LearningCenter.Domain.Factories.Achievements
{
    internal class AchievementFactory : IAchievementFactory
    {
        private string _name;
        private int _goal;
        private AchievementUnitType _unitType;
        private int _targetId;
        private bool _targetSet = false;

        public IAchievementFactory WithGoal(int goal)
        {
            _goal = goal;
            return this;
        }

        public IAchievementFactory WithName(string name)
        {
            _name = name;
            return this;
        }

        public IAchievementFactory WithTarget(int targetId)
        {
            _targetId = targetId;
            _targetSet = true;
            return this;
        }

        public IAchievementFactory WithUnitType(AchievementUnitType unitType)
        {
            _unitType = unitType;
            return this;
        }

        public Achievement Build()
        {
            if (_targetSet == true)
            {
                return new Achievement(
                    _name,
                    _unitType,
                    _targetId);
            }

            return new Achievement(
                _name,
                _goal,
                _unitType);
        }

        public Achievement BuildGeneral(string name,  int goal, AchievementUnitType unitType)
        {
            return this
                .WithName(name)
                .WithGoal(goal)
                .WithUnitType(unitType)
                .Build();
        }

        public Achievement BuildSpecific(string name, AchievementUnitType unitType, int targetId)
        {
            return this
                .WithName(name)
                .WithUnitType(unitType)
                .WithTarget(targetId)
                .Build();
        }
    }
}
