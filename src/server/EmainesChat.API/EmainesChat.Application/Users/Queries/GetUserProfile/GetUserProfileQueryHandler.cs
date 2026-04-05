using EmainesChat.Application.DTOs;
using EmainesChat.Domain.Aggregates.Users;
using EmainesChat.Domain.Common;
using MediatR;

namespace EmainesChat.Application.Users.Queries.GetUserProfile;

public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, Result<UserDto>>
{
    private readonly IUserRepository _userRepository;

    public GetUserProfileQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<UserDto>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user is null) return Result.Failure<UserDto>("Usuário não encontrado.");
        return Result.Success(UserDto.From(user));
    }
}
