using EmainesChat.Application.Common;
using EmainesChat.Application.DTOs;
using EmainesChat.Domain.Aggregates.Rooms;
using EmainesChat.Domain.Common;
using MediatR;

namespace EmainesChat.Application.Rooms.Commands.UpdateRoom;

public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, Result<RoomDto>>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateRoomCommandHandler(IRoomRepository roomRepository, IUnitOfWork unitOfWork)
    {
        _roomRepository = roomRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RoomDto>> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
    {
        var room = await _roomRepository.GetByIdAsync(request.Id);
        if (room is null)
            return Result.Failure<RoomDto>("Sala não encontrada.");

        var updateResult = room.UpdateName(request.Name);
        if (updateResult.IsFailure)
            return Result.Failure<RoomDto>(updateResult.Error);

        _roomRepository.Update(room);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(RoomDto.From(room));
    }
}
