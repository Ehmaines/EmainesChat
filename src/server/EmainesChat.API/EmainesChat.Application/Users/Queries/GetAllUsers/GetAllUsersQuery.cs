using EmainesChat.Application.DTOs;
using MediatR;

namespace EmainesChat.Application.Users.Queries.GetAllUsers;

public record GetAllUsersQuery : IRequest<IReadOnlyList<UserDto>>;
