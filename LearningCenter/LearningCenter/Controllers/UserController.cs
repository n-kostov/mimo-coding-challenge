using LearningCenter.Domain.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace LearningCenter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private static readonly User user = new User("John Doe");

        [HttpPost("complete-lesson")]
        public async Task<IActionResult> CompleteLesson(int userId, [FromBody] object request)
        {
            user.AddLessonCompleted(1, DateTime.UtcNow, DateTime.UtcNow.AddHours(1));

            return Ok();
        }

        [HttpGet("achievements")]
        public async Task<IActionResult> GetUserAchievements(int userId)
        {
            user.AddAchievement(1, true, 100);
            user.AddAchievement(2, false, 3);

            return Ok(user.Achievements);
        }
    }
}
