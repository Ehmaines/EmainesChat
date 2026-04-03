using EmainesChat.Application.DTOs;
using EmainesChat.Domain.Aggregates.Rooms;
using MediatR;

namespace EmainesChat.Application.Rooms.Queries.GetAllRooms;

public class GetAllRoomsQueryHandler : IRequestHandler<GetAllRoomsQuery, IReadOnlyList<RoomDto>>
{
    private readonly IRoomRepository _roomRepository;

    public GetAllRoomsQueryHandler(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<IReadOnlyList<RoomDto>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
    {
        var rooms = await _roomRepository.GetAllAsync();
        return rooms.Select(RoomDto.From).ToList();
    }
}
