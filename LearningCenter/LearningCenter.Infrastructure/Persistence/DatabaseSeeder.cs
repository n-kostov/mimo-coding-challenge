using LearningCenter.Domain.Factories.Achievements;
using LearningCenter.Domain.Factories.Courses;
using LearningCenter.Domain.Factories.Users;
using LearningCenter.Domain.Models.Achievements;

namespace LearningCenter.Infrastructure.Persistence
{
    internal class DatabaseSeeder : IDatabaseSeeder
    {
        private readonly LearningCenterDbContext _db;
        private readonly IUserFactory _userFactory;
        private readonly ICourseFactory _courseFactory;
        private readonly IAchievementFactory _achievementFactory;

        public DatabaseSeeder(
            LearningCenterDbContext db,
            IUserFactory userFactory,
            ICourseFactory courseFactory,
            IAchievementFactory achievementFactory)
        {
            _db = db;
            _userFactory = userFactory;
            _courseFactory = courseFactory;
            _achievementFactory = achievementFactory;
        }

        public void Seed()
        {
            var user = _userFactory
                .WithName("Jon")
                .Build();
            _db.Users.Add(user);

            var cCharpCourse = _courseFactory
                .WithName("C#")
                .WithChapter("Chapter 1")
                    .WithLesson("Chapter 1", "Variables")
                    .WithLesson("Chapter 1", "Data Types")
                .Build();
            _db.Courses.Add(cCharpCourse);

            var switftCourse = _courseFactory
                .WithName("Swift")
                .WithChapter("Chapter 1")
                    .WithLesson("Chapter 1", "What is swift")
                    .WithLesson("Chapter 1", "Hello world")
                .WithChapter("Chapter 2")
                    .WithLesson("Chapter 2", "Variables")
                    .WithLesson("Chapter 2", "Functions")
                .Build();
            _db.Courses.Add(switftCourse);
            _db.SaveChanges();

            _db.Achievements.Add(_achievementFactory
                .WithName("Complete 5 lessons")
                .WithUnitType(AchievementUnitType.Lesson)
                .WithGoal(5)
                .Build());

            _db.Achievements.Add(_achievementFactory
                .WithName("Complete 25 lessons")
                .WithUnitType(AchievementUnitType.Lesson)
                .WithGoal(25)
                .Build());

            _db.Achievements.Add(_achievementFactory
                .WithName("Complete 50 lessons")
                .WithUnitType(AchievementUnitType.Lesson)
                .WithGoal(50)
                .Build());

            _db.Achievements.Add(_achievementFactory
                .WithName("Complete 1 chapter")
                .WithUnitType(AchievementUnitType.Chapter)
                .WithGoal(1)
                .Build());

            _db.Achievements.Add(_achievementFactory
                .WithName("Complete 5 chapters")
                .WithUnitType(AchievementUnitType.Chapter)
                .WithGoal(5)
                .Build());

            _db.Achievements.Add(_achievementFactory
                .WithName("Complete the Swift course")
                .WithUnitType(AchievementUnitType.Course)
                .WithTarget(switftCourse.Id)
                .Build());

            _db.Achievements.Add(_achievementFactory
                .WithName("Complete the C# course")
                .WithUnitType(AchievementUnitType.Course)
                .WithTarget(cCharpCourse.Id)
                .Build());

            _db.SaveChanges();
        }
    }
}
