using EmainesChat.Application.DTOs;
using EmainesChat.Domain.Common;
using MediatR;

namespace EmainesChat.Application.Users.Commands.UpdateUserPicture;

public record UpdateUserPictureCommand(Guid UserId, string? Url) : IRequest<Result<UserDto>>;
