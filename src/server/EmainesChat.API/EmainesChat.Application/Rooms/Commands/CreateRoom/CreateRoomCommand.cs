using EmainesChat.Application.DTOs;
using EmainesChat.Domain.Common;
using MediatR;

namespace EmainesChat.Application.Rooms.Commands.CreateRoom;

public record CreateRoomCommand(string Name) : IRequest<Result<RoomDto>>;
