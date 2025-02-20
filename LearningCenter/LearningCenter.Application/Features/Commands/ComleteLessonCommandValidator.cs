using FluentValidation;

namespace LearningCenter.Application.Features.Commands
{
    public class ComleteLessonCommandValidator : AbstractValidator<CompleteLessonCommand>
    {
        public ComleteLessonCommandValidator(ICourseRepository courseRepository) 
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.LessonId)
                .NotEmpty()
                .GreaterThan(0)
                .Must((lessonId) => courseRepository.DoesLessonExistAsync(lessonId).Result)
                .WithMessage("Lesson with the provided id does not exist");
        }
    }
}
