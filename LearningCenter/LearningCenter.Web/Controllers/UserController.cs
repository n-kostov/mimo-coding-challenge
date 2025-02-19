using LearningCenter.Application.Features.Commands;
using LearningCenter.Application.Features.Queries;
using LearningCenter.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace LearningCenter.Controllers
{
    public class UserController : ApiController
    {

        [HttpPost("complete-lesson")]
        public async Task<IActionResult> CompleteLesson([FromBody] CompleteLessonCommand completeLessonCommand)
        {
            return await Send(completeLessonCommand);
        }

        [HttpGet("achievements")]
        public async Task<ActionResult<GetUserAchievementsOutputModel>> GetUserAchievements([FromQuery] GetUserAchievementsQuery query)
        {
            query.UserId = 1;
            return await Send(query);
        }
    }
}
