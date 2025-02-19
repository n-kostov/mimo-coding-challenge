using LearningCenter.Application.Contracts;
using LearningCenter.Application.Features.Queries;
using LearningCenter.Domain.Models.Users;

namespace LearningCenter.Application.Features
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> FindByIdAsync(int userId);
        Task<IEnumerable<UserAchievementListingModel>> GetUserAchievements(int userId);

        Task SaveChangesAsync();
    }
}
