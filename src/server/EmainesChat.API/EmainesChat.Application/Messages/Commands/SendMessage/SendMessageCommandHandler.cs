using EmainesChat.Application.Common;
using EmainesChat.Application.DTOs;
using EmainesChat.Application.Messages.Notifications;
using EmainesChat.Domain.Aggregates.Messages;
using EmainesChat.Domain.Aggregates.Rooms;
using EmainesChat.Domain.Aggregates.Users;
using EmainesChat.Domain.Common;
using MediatR;

namespace EmainesChat.Application.Messages.Commands.SendMessage;

public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, Result<MessageDto>>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IUserRepository _userRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisher _publisher;

    public SendMessageCommandHandler(
        IMessageRepository messageRepository,
        IUserRepository userRepository,
        IRoomRepository roomRepository,
        IUnitOfWork unitOfWork,
        IPublisher publisher)
    {
        _messageRepository = messageRepository;
        _userRepository = userRepository;
        _roomRepository = roomRepository;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task<Result<MessageDto>> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user is null)
            return Result.Failure<MessageDto>("Usuário não encontrado.");

        var room = await _roomRepository.GetByIdAsync(request.RoomId);
        if (room is null)
            return Result.Failure<MessageDto>("Sala não encontrada.");

        var messageResult = Message.Create(request.Content, user, room);
        if (messageResult.IsFailure)
            return Result.Failure<MessageDto>(messageResult.Error);

        var message = messageResult.Value;
        await _messageRepository.AddAsync(message);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _publisher.Publish(
            new MessageCreatedNotification(message.Id, message.RoomId, message.UserId, message.Content.Value, message.User.UserName, message.SentAt, message.User.ProfilePictureUrl),
            cancellationToken);

        return Result.Success(MessageDto.From(message));
    }
}
