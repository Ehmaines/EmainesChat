using EmainesChat.Application.DTOs;
using EmainesChat.Domain.Aggregates.Messages;
using MediatR;

namespace EmainesChat.Application.Messages.Queries.GetMessagesByRoom;

public class GetMessagesByRoomQueryHandler : IRequestHandler<GetMessagesByRoomQuery, IReadOnlyList<MessageDto>>
{
    private readonly IMessageRepository _messageRepository;

    public GetMessagesByRoomQueryHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<IReadOnlyList<MessageDto>> Handle(GetMessagesByRoomQuery request, CancellationToken cancellationToken)
    {
        var messages = await _messageRepository.GetByRoomIdAsync(request.RoomId);
        return messages.Select(MessageDto.From).ToList();
    }
}
