using System.Text.RegularExpressions;
using EmainesChat.Domain.Common;

namespace EmainesChat.Domain.ValueObjects;

public sealed class Name
{
    public string Value { get; }

    private Name(string value) => Value = value;

    public static Result<Name> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure<Name>("O nome não pode ser vazio.");
        var trimmed = value.Trim();
        if (trimmed.Length < 3)
            return Result.Failure<Name>("O nome deve ter no mínimo 3 caracteres.");
        if (trimmed.Length > 100)
            return Result.Failure<Name>("O nome deve ter no máximo 100 caracteres.");
        if (Regex.IsMatch(trimmed, @"[^\p{L} ]"))
            return Result.Failure<Name>("O nome não pode conter números ou caracteres especiais.");
        return Result.Success(new Name(trimmed));
    }
}
