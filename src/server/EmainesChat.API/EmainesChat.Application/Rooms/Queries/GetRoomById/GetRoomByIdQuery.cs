using EmainesChat.Application.DTOs;
using EmainesChat.Domain.Common;
using MediatR;

namespace EmainesChat.Application.Rooms.Queries.GetRoomById;

public record GetRoomByIdQuery(Guid Id) : IRequest<Result<RoomDto>>;
