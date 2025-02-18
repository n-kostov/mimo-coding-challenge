using LearningCenter.Domain.Models.Users;

namespace LearningCenter.Domain.Factories.Users
{
    public interface IUserFactory : IFactory<User>
    {
        IUserFactory WithName(string name);
    }
}
