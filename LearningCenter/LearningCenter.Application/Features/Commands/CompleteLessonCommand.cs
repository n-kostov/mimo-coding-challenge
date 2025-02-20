using LearningCenter.Application.Features.Events;
using MediatR;

namespace LearningCenter.Application.Features.Commands
{
    public class CompleteLessonCommand : CompleteLessonInputModel, IRequest<Result>
    {
        public CompleteLessonCommand(int userId, int lessonId, DateTime startedOn, DateTime completedOn) 
            : base(userId, lessonId, startedOn, completedOn)
        {
        }

        public class CompleteLessonCommandHandler: IRequestHandler<CompleteLessonCommand, Result>
        {
            private const string UserNotFoundErrorMessage = "User not found.";
            private readonly IUserRepository _userRepository;
            private readonly IMediator _mediator;
            public CompleteLessonCommandHandler(IUserRepository userRepository, IMediator mediator)
            {
                _userRepository = userRepository;
                _mediator = mediator;
            }

            public async Task<Result> Handle(CompleteLessonCommand request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.FindByIdAsync(request.UserId);
                if (user == null)
                {
                    return UserNotFoundErrorMessage;
                }

                user.AddLessonCompleted(request.LessonId, request.StartedOn, request.CompletedOn);
                await _userRepository.SaveChangesAsync();

                return Result.Success;
            }
        }

    }
}
