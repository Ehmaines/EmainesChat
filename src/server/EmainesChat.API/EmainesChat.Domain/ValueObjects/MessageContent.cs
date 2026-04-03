using EmainesChat.Domain.Common;

namespace EmainesChat.Domain.ValueObjects;

public sealed class MessageContent
{
    public string Value { get; }

    private MessageContent(string value) => Value = value;

    public static Result<MessageContent> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure<MessageContent>("Conteúdo da mensagem não pode ser vazio.");
        if (value.Length > 2000)
            return Result.Failure<MessageContent>("Mensagem não pode ter mais de 2000 caracteres.");
        return Result.Success(new MessageContent(value.Trim()));
    }
}
