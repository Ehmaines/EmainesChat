using EmainesChat.Application.DTOs;
using EmainesChat.Domain.Common;
using MediatR;

namespace EmainesChat.Application.Users.Commands.AuthenticateUser;

public record AuthenticateUserCommand(string Email, string Password) : IRequest<Result<AuthResultDto>>;
