using MediatR;

namespace LearningCenter.Application.Features.Queries
{
    public class GetUserAchievementsQuery : IRequest<GetUserAchievementsOutputModel>
    {
        public int UserId { get; set; }

        public class GetUserAchievementsQueryHandler : IRequestHandler<GetUserAchievementsQuery, GetUserAchievementsOutputModel>
        {
            private readonly IUserRepository _userRepository;

            public GetUserAchievementsQueryHandler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<GetUserAchievementsOutputModel> Handle(GetUserAchievementsQuery request, CancellationToken cancellationToken)
            {
                var userAchievements = await _userRepository.GetUserAchievements(request.UserId);
                return new GetUserAchievementsOutputModel(userAchievements);
            }
        }
    }
}
