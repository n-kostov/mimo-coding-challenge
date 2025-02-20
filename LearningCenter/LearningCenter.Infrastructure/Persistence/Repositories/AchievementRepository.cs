using LearningCenter.Application.Features;
using LearningCenter.Domain.Models.Achievements;
using Microsoft.EntityFrameworkCore;

namespace LearningCenter.Infrastructure.Persistence.Repositories
{
    internal class AchievementRepository : DataRepository<Achievement>, IAchievementRepository
    {
        public AchievementRepository(LearningCenterDbContext db)
            : base(db)
        {
        }

        public async Task<IEnumerable<Achievement>> GetAllAsync()
        {
            return await this.All().ToListAsync();
        }
    }
}
