using EmainesChat.Application.Common;
using EmainesChat.Application.DTOs;
using EmainesChat.Domain.Aggregates.Users;
using EmainesChat.Domain.Common;
using MediatR;

namespace EmainesChat.Application.Users.Commands.UpdateUserPicture;

public class UpdateUserPictureCommandHandler : IRequestHandler<UpdateUserPictureCommand, Result<UserDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserPictureCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<UserDto>> Handle(UpdateUserPictureCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user is null)
            return Result.Failure<UserDto>("Usuário não encontrado.");

        var result = user.UpdateProfilePicture(request.Url);
        if (result.IsFailure)
            return Result.Failure<UserDto>(result.Error);

        await _userRepository.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(UserDto.From(user));
    }
}
