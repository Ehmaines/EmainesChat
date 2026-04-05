using EmainesChat.Application.Common;
using EmainesChat.Application.DTOs;
using EmainesChat.Domain.Aggregates.Users;
using EmainesChat.Domain.Common;
using MediatR;

namespace EmainesChat.Application.Users.Commands.UpdateUserProfile;

public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, Result<UserDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserProfileCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<UserDto>> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user is null)
            return Result.Failure<UserDto>("Usuário não encontrado.");

        if (request.Name is not null)
        {
            var result = user.UpdateName(request.Name);
            if (result.IsFailure) return Result.Failure<UserDto>(result.Error);
        }

        if (request.Email is not null)
        {
            var existing = await _userRepository.GetByEmailAsync(request.Email);
            if (existing is not null && existing.Id != user.Id)
                return Result.Failure<UserDto>("Este email já está em uso.");

            var result = user.UpdateEmail(request.Email);
            if (result.IsFailure) return Result.Failure<UserDto>(result.Error);
        }

        if (request.NewPassword is not null)
        {
            var result = user.UpdatePassword(request.CurrentPassword!, request.NewPassword);
            if (result.IsFailure) return Result.Failure<UserDto>(result.Error);
        }

        await _userRepository.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(UserDto.From(user));
    }
}
