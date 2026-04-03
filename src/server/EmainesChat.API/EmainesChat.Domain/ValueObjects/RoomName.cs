using EmainesChat.Domain.Common;

namespace EmainesChat.Domain.ValueObjects;

public sealed class RoomName
{
    public string Value { get; }

    private RoomName(string value) => Value = value;

    public static Result<RoomName> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure<RoomName>("Nome da sala não pode ser vazio.");
        if (value.Length > 100)
            return Result.Failure<RoomName>("Nome da sala não pode ter mais de 100 caracteres.");
        return Result.Success(new RoomName(value.Trim()));
    }
}
