using System.Text.RegularExpressions;
using EmainesChat.Domain.Common;

namespace EmainesChat.Domain.ValueObjects;

public sealed class Password
{
    public string Hash { get; }

    private Password(string hash) => Hash = hash;

    // Usado somente para carregar do banco — não valida
    public static Password FromHash(string hash) => new(hash);

    // Factory validada — único ponto de entrada para senhas vindas do usuário
    public static Result<Password> Create(string plainText)
    {
        if (string.IsNullOrWhiteSpace(plainText))
            return Result.Failure<Password>("A senha não pode ser vazia.");
        if (plainText.Length < 8)
            return Result.Failure<Password>("A senha deve ter no mínimo 8 caracteres.");
        if (!Regex.IsMatch(plainText, @"[a-zA-Z]") || !Regex.IsMatch(plainText, @"[0-9]"))
            return Result.Failure<Password>("A senha deve conter pelo menos uma letra e um número.");
        return Result.Success(new Password(BCrypt.Net.BCrypt.HashPassword(plainText)));
    }

    public bool Verify(string plainText) => BCrypt.Net.BCrypt.Verify(plainText, Hash);
}
