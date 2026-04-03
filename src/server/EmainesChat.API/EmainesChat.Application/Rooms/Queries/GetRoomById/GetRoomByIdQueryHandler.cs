using EmainesChat.Application.DTOs;
using EmainesChat.Domain.Aggregates.Rooms;
using EmainesChat.Domain.Common;
using MediatR;

namespace EmainesChat.Application.Rooms.Queries.GetRoomById;

public class GetRoomByIdQueryHandler : IRequestHandler<GetRoomByIdQuery, Result<RoomDto>>
{
    private readonly IRoomRepository _roomRepository;

    public GetRoomByIdQueryHandler(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<Result<RoomDto>> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
    {
        var room = await _roomRepository.GetByIdAsync(request.Id);
        if (room is null)
            return Result.Failure<RoomDto>("Sala não encontrada.");

        return Result.Success(RoomDto.From(room));
    }
}
