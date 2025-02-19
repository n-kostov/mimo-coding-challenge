using LearningCenter.Application.Features;
using LearningCenter.Application.Features.Queries;
using LearningCenter.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace LearningCenter.Infrastructure.Persistence.Repositories
{
    internal class UserRepository : DataRepository<User>, IUserRepository
    {
        public UserRepository(LearningCenterDbContext db)
            : base(db)
        {
        }

        public async Task<User> FindByIdAsync(int userId)
        {
            return await this.All()
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<IEnumerable<UserAchievementListingModel>> GetUserAchievements(int userId)
        {
            return await this.All()
                .Where(u => u.Id == userId)
                .SelectMany(u => u.Achievements)
                .Select(a => new UserAchievementListingModel(a.AchievementId, a.IsCompleted, a.Progress))
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await this.SaveChanges();
        }
    }
}
