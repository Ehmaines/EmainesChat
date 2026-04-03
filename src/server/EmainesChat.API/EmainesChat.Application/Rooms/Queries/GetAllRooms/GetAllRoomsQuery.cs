using EmainesChat.Application.DTOs;
using MediatR;

namespace EmainesChat.Application.Rooms.Queries.GetAllRooms;

public record GetAllRoomsQuery : IRequest<IReadOnlyList<RoomDto>>;
