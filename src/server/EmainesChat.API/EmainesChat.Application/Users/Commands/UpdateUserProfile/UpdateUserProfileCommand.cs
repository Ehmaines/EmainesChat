using EmainesChat.Application.DTOs;
using EmainesChat.Domain.Common;
using MediatR;

namespace EmainesChat.Application.Users.Commands.UpdateUserProfile;

public record UpdateUserProfileCommand(
    Guid UserId,
    string? Name,
    string? Email,
    string? CurrentPassword,
    string? NewPassword
) : IRequest<Result<UserDto>>;
