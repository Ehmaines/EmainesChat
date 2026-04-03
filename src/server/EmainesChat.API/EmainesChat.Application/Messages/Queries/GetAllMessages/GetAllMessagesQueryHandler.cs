using EmainesChat.Application.DTOs;
using EmainesChat.Domain.Aggregates.Messages;
using MediatR;

namespace EmainesChat.Application.Messages.Queries.GetAllMessages;

public class GetAllMessagesQueryHandler : IRequestHandler<GetAllMessagesQuery, IReadOnlyList<MessageDto>>
{
    private readonly IMessageRepository _messageRepository;

    public GetAllMessagesQueryHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<IReadOnlyList<MessageDto>> Handle(GetAllMessagesQuery request, CancellationToken cancellationToken)
    {
        var messages = await _messageRepository.GetAllAsync();
        return messages.Select(MessageDto.From).ToList();
    }
}
