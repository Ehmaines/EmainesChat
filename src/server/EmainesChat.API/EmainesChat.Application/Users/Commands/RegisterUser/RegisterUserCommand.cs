using EmainesChat.Application.DTOs;
using EmainesChat.Domain.Common;
using MediatR;

namespace EmainesChat.Application.Users.Commands.RegisterUser;

public record RegisterUserCommand(string UserName, string? Name, string Email, string Password) : IRequest<Result<UserDto>>;
