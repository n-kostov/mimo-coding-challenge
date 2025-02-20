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
                .Join(
                    this.db.Achievements,
                    userAchievement => userAchievement.AchievementId,
                    achievement => achievement.Id,
                    (userAchievement, achievement) => new UserAchievementListingModel(
                        userAchievement.AchievementId,
                        achievement.Name,
                        userAchievement.IsCompleted,
                        achievement.Goal,
                        userAchievement.Progress
                        )
                )
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await this.SaveChanges();
        }
    }
}
