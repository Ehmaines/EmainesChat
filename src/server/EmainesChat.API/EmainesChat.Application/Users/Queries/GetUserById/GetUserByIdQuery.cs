using EmainesChat.Application.DTOs;
using EmainesChat.Domain.Common;
using MediatR;

namespace EmainesChat.Application.Users.Queries.GetUserById;

public record GetUserByIdQuery(Guid Id) : IRequest<Result<UserDto>>;
