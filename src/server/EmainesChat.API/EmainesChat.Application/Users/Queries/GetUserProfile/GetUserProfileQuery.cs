using EmainesChat.Application.DTOs;
using EmainesChat.Domain.Common;
using MediatR;

namespace EmainesChat.Application.Users.Queries.GetUserProfile;

public record GetUserProfileQuery(Guid UserId) : IRequest<Result<UserDto>>;
