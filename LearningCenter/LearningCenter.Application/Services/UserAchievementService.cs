using LearningCenter.Application.Contracts;
using LearningCenter.Application.Features;
using LearningCenter.Domain.Exceptions;
using LearningCenter.Domain.Models.Achievements;
using LearningCenter.Domain.Models.Courses;
using LearningCenter.Domain.Models.Users;

namespace LearningCenter.Application.Services
{
    public class UserAchievementService : IUserAchievementService
    {
        private const string UserNotFoundErrorMessage = "User not found.";
        private readonly IUserRepository _userRepository;
        private readonly IAchievementRepository _achievementRepository;
        private readonly ICourseRepository _courseRepository;

        public UserAchievementService(
            IUserRepository userRepository,
            IAchievementRepository achievementRepository,
            ICourseRepository courseRepository)
        {
            _userRepository = userRepository;
            _achievementRepository = achievementRepository;
            _courseRepository = courseRepository;
        }

        public async Task TrackLessonCompletion(int userId, int lessonId)
        {
            var user = await _userRepository.FindByIdAsync(userId)
                       ?? throw new InvalidUserException(UserNotFoundErrorMessage);

            var achievements = await _achievementRepository.GetAllAsync();
            var courses = await _courseRepository.GetAllAsync();
            var timesCompletedLesson = user.LessonsCompleted.Count(l => l.LessonId == lessonId);
            var lessonCourse = courses.FirstOrDefault(c => c.Chapters.Any(ch => ch.Lessons.Any(l => l.Id == lessonId)));

            HandleLessonAchievements(user, achievements, lessonId, timesCompletedLesson);
            HandleChapterAchievements(user, achievements, lessonCourse);
            HandleCourseAchievements(user, achievements, lessonCourse, courses);

            await _userRepository.SaveChangesAsync();
        }

        private void HandleLessonAchievements(User user, IEnumerable<Achievement> achievements, int lessonId, int timesCompletedLesson)
        {
            var lessonAchievements = achievements.Where(a => a.UnitType == AchievementUnitType.Lesson);

            foreach (var achievement in lessonAchievements.Where(a => a.TargetId.HasValue))
            {
                if (achievement.TargetId == lessonId)
                {
                    HandleSpecificAchievement(user, achievement);
                }
            }

            var completedLessons = user.LessonsCompleted.Select(l => l.LessonId).Distinct().Count();
            HandleGenericAchievements(user, lessonAchievements.Where(a => !a.TargetId.HasValue), completedLessons);
        }

        private void HandleChapterAchievements(User user, IEnumerable<Achievement> achievements, Course course)
        {
            var chapterAchievements = achievements.Where(a => a.UnitType == AchievementUnitType.Chapter);

            foreach (var achievement in chapterAchievements.Where(a => a.TargetId.HasValue))
            {
                if (IsUnitCompleted(user, course, achievement.TargetId.Value))
                {
                    HandleSpecificAchievement(user, achievement);
                }
            }

            var completedChapters = FindCompletedChapters(user, course);
            HandleGenericAchievements(user, chapterAchievements.Where(a => !a.TargetId.HasValue), completedChapters);
        }

        private void HandleCourseAchievements(User user, IEnumerable<Achievement> achievements, Course lessonCourse, IEnumerable<Course> courses)
        {
            var courseAchievements = achievements.Where(a => a.UnitType == AchievementUnitType.Course);

            foreach (var achievement in courseAchievements.Where(a => a.TargetId.HasValue && a.TargetId == lessonCourse.Id))
            {
                if (IsUnitCompleted(user, lessonCourse, null)) // Pass `null` to check the entire course
                {
                    HandleSpecificAchievement(user, achievement);
                }
            }

            var completedCourses = FindCompletedCourses(user, courses);
            HandleGenericAchievements(user, courseAchievements.Where(a => !a.TargetId.HasValue), completedCourses);
        }

        private void HandleSpecificAchievement(User user, Achievement achievement)
        {
            if (!user.Achievements.Any(a => a.AchievementId == achievement.Id))
            {
                user.AddAchievement(achievement.Id, true, 1);
            }
        }

        private void HandleGenericAchievements(User user, IEnumerable<Achievement> genericAchievements, int progressIncrement)
        {
            if (progressIncrement <= 0)
            {
                return;
            }

            foreach (var achievement in genericAchievements)
            {
                var userAchievement = user.Achievements.FirstOrDefault(a => a.AchievementId == achievement.Id);

                if (userAchievement == null)
                {
                    user.AddAchievement(achievement.Id, achievement.Goal == progressIncrement, progressIncrement);
                }
                else if (!userAchievement.IsCompleted)
                {
                    userAchievement.UpdateProgress(progressIncrement >= achievement.Goal);
                }
            }
        }

        private int FindCompletedChapters(User user, Course course)
        {
            var lessonsPerChapter = course.Chapters.GroupBy(ch => ch.Id, c => c.Lessons.Select(l => l.Id));

            if (lessonsPerChapter == null) return 0;

            return lessonsPerChapter.Count(ch => ch.SelectMany(l => l).All(lessonId => user.LessonsCompleted.Any(lc => lc.LessonId == lessonId)));
        }

        private int FindCompletedCourses(User user, IEnumerable<Course> courses)
        {
            return courses.Count(c => IsUnitCompleted(user, c, null));
        }

        private bool IsUnitCompleted(User user, Course course, int? unitId)
        {
            var lessonsInUnit = unitId.HasValue
                ? course.Chapters.FirstOrDefault(c => c.Id == unitId)?.Lessons.Select(l => l.Id)
                : course.Chapters.SelectMany(c => c.Lessons).Select(l => l.Id); // Check all lessons in the course

            if (lessonsInUnit == null || !lessonsInUnit.Any()) return false;

            return lessonsInUnit.All(l => user.LessonsCompleted.Any(lc => lc.LessonId == l));
        }
    }
}
