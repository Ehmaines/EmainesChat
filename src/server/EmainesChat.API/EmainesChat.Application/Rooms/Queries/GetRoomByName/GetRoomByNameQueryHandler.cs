using EmainesChat.Application.DTOs;
using EmainesChat.Domain.Aggregates.Rooms;
using EmainesChat.Domain.Common;
using MediatR;

namespace EmainesChat.Application.Rooms.Queries.GetRoomByName;

public class GetRoomByNameQueryHandler : IRequestHandler<GetRoomByNameQuery, Result<RoomDto>>
{
    private readonly IRoomRepository _roomRepository;

    public GetRoomByNameQueryHandler(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<Result<RoomDto>> Handle(GetRoomByNameQuery request, CancellationToken cancellationToken)
    {
        var room = await _roomRepository.GetByNameAsync(request.Name);
        if (room is null)
            return Result.Failure<RoomDto>("Sala não encontrada.");

        return Result.Success(RoomDto.From(room));
    }
}
