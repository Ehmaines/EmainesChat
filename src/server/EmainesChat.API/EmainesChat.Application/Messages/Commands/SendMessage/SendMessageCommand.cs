using EmainesChat.Application.DTOs;
using EmainesChat.Domain.Common;
using MediatR;

namespace EmainesChat.Application.Messages.Commands.SendMessage;

public record SendMessageCommand(string Content, Guid UserId, Guid RoomId) : IRequest<Result<MessageDto>>;
