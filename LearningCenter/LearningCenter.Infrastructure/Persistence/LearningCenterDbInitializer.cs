using Microsoft.EntityFrameworkCore;

namespace LearningCenter.Infrastructure.Persistence
{
    internal class LearningCenterDbInitializer : IInitializer
    {
        private readonly LearningCenterDbContext db;

        public LearningCenterDbInitializer(LearningCenterDbContext db) => this.db = db;

        public void Initialize() => this.db.Database.Migrate();
    }
}
