using EmainesChat.Application.DTOs;
using EmainesChat.Domain.Common;
using MediatR;

namespace EmainesChat.Application.Messages.Commands.SendMessage;

public record SendMessageCommand(string Content, int UserId, int RoomId) : IRequest<Result<MessageDto>>;
