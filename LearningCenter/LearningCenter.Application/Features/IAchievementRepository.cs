using LearningCenter.Application.Contracts;
using LearningCenter.Domain.Models.Achievements;

namespace LearningCenter.Application.Features
{
    public interface IAchievementRepository : IRepository<Achievement>
    {
        Task<IEnumerable<Achievement>> GetAllAsync();
    }
}
