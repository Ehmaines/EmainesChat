using EmainesChat.Application.Common;
using EmainesChat.Application.DTOs;
using EmainesChat.Domain.Aggregates.Rooms;
using EmainesChat.Domain.Common;
using MediatR;

namespace EmainesChat.Application.Rooms.Commands.CreateRoom;

public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, Result<RoomDto>>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateRoomCommandHandler(IRoomRepository roomRepository, IUnitOfWork unitOfWork)
    {
        _roomRepository = roomRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RoomDto>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        var existing = await _roomRepository.GetByNameAsync(request.Name);
        if (existing is not null)
            return Result.Failure<RoomDto>("Já existe uma sala com esse nome.");

        var roomResult = Room.Create(request.Name);
        if (roomResult.IsFailure)
            return Result.Failure<RoomDto>(roomResult.Error);

        await _roomRepository.AddAsync(roomResult.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(RoomDto.From(roomResult.Value));
    }
}
