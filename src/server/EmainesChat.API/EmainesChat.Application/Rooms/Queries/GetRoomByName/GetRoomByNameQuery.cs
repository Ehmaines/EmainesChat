using EmainesChat.Application.DTOs;
using EmainesChat.Domain.Common;
using MediatR;

namespace EmainesChat.Application.Rooms.Queries.GetRoomByName;

public record GetRoomByNameQuery(string Name) : IRequest<Result<RoomDto>>;
