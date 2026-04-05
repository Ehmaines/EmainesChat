using EmainesChat.Application.DTOs;
using MediatR;

namespace EmainesChat.Application.Messages.Queries.GetMessagesByRoom;

public record GetMessagesByRoomQuery(Guid RoomId) : IRequest<IReadOnlyList<MessageDto>>;
