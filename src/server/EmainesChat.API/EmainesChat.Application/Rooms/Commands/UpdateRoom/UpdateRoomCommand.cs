using EmainesChat.Application.DTOs;
using EmainesChat.Domain.Common;
using MediatR;

namespace EmainesChat.Application.Rooms.Commands.UpdateRoom;

public record UpdateRoomCommand(Guid Id, string Name) : IRequest<Result<RoomDto>>;
