using EmainesChat.Application.Common;
using EmainesChat.Application.DTOs;
using EmainesChat.Domain.Aggregates.Users;
using EmainesChat.Domain.Common;
using MediatR;

namespace EmainesChat.Application.Users.Commands.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<UserDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<UserDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var existing = await _userRepository.GetByEmailAsync(request.Email);
        if (existing is not null)
            return Result.Failure<UserDto>("Email já está em uso.");

        var existingUserName = await _userRepository.GetByUserNameAsync(request.UserName);
        if (existingUserName is not null)
            return Result.Failure<UserDto>("Nome de usuário já está em uso.");

        var userResult = User.Create(request.UserName, request.Email, request.Password);
        if (userResult.IsFailure)
            return Result.Failure<UserDto>(userResult.Error);

        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            var nameResult = userResult.Value.UpdateName(request.Name);
            if (nameResult.IsFailure)
                return Result.Failure<UserDto>(nameResult.Error);
        }

        await _userRepository.AddAsync(userResult.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(UserDto.From(userResult.Value));
    }
}
