using LearningCenter.Domain.Models.Users;

namespace LearningCenter.Domain.Factories.Users
{
    internal class UserFactory : IUserFactory
    {
        private string _name;

        public IUserFactory WithName(string name)
        {
            _name = name;
            return this;
        }

        public User Build()
        {
            return new User(_name);
        }
    }
}
