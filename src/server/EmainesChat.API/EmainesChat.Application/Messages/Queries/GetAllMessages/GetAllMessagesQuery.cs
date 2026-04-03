using EmainesChat.Application.DTOs;
using MediatR;

namespace EmainesChat.Application.Messages.Queries.GetAllMessages;

public record GetAllMessagesQuery : IRequest<IReadOnlyList<MessageDto>>;
