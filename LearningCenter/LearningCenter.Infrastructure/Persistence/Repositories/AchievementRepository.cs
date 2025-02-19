using LearningCenter.Application.Features;
using LearningCenter.Domain.Models.Achievements;

namespace LearningCenter.Infrastructure.Persistence.Repositories
{
    internal class AchievementRepository : DataRepository<Achievement>, IAchievementRepository
    {
        public AchievementRepository(LearningCenterDbContext db)
            : base(db)
        {
        }

        public IEnumerable<Achievement> GetAll()
        {
            return this.All().ToList();
        }
    }
}
