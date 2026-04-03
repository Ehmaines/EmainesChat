using System.Text.RegularExpressions;
using EmainesChat.Domain.Common;

namespace EmainesChat.Domain.ValueObjects;

public sealed class Email
{
    public string Value { get; }

    private Email(string value) => Value = value;

    public static Result<Email> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure<Email>("Email não pode ser vazio.");
        if (!Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            return Result.Failure<Email>("Email inválido.");
        return Result.Success(new Email(value.ToLowerInvariant()));
    }
}
