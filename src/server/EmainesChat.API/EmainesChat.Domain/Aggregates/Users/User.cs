using EmainesChat.Domain.Common;
using EmainesChat.Domain.Enums;
using EmainesChat.Domain.ValueObjects;
using ValueObjects = EmainesChat.Domain.ValueObjects;

namespace EmainesChat.Domain.Aggregates.Users;

public class User
{
    public Guid Id { get; private set; }
    public string UserName { get; private set; } = null!;
    public ValueObjects.Name? Name { get; private set; }
    public Email Email { get; private set; } = null!;
    public Password Password { get; private set; } = null!;
    public string? ProfilePictureUrl { get; private set; }
    public Roles Role { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private User() { } // EF Core

    public static Result<User> Create(string userName, string email, string plainPassword)
    {
        if (string.IsNullOrWhiteSpace(userName))
            return Result.Failure<User>("Nome de usuário não pode ser vazio.");

        var emailResult = Email.Create(email);
        if (emailResult.IsFailure)
            return Result.Failure<User>(emailResult.Error);

        var passwordResult = Password.Create(plainPassword);
        if (passwordResult.IsFailure)
            return Result.Failure<User>(passwordResult.Error);

        return Result.Success(new User
        {
            Id = Guid.NewGuid(),
            UserName = userName,
            Email = emailResult.Value,
            Password = passwordResult.Value,
            Role = Roles.User,
            CreatedAt = DateTime.UtcNow
        });
    }

    public Result UpdateName(string name)
    {
        var result = ValueObjects.Name.Create(name);
        if (result.IsFailure) return Result.Failure(result.Error);
        Name = result.Value;
        return Result.Success();
    }

    public Result UpdateEmail(string email)
    {
        var result = Email.Create(email);
        if (result.IsFailure) return Result.Failure(result.Error);
        Email = result.Value;
        return Result.Success();
    }

    public Result UpdatePassword(string currentPlainText, string newPlainText)
    {
        if (!Password.Verify(currentPlainText))
            return Result.Failure("Senha atual incorreta.");

        var result = Password.Create(newPlainText);
        if (result.IsFailure) return Result.Failure(result.Error);
        Password = result.Value;
        return Result.Success();
    }

    public Result UpdateProfilePicture(string? url)
    {
        if (url is not null)
        {
            if (url.Length > 2048)
                return Result.Failure("URL da foto excede o tamanho máximo de 2048 caracteres.");
            if (!Uri.TryCreate(url, UriKind.Absolute, out var uri) ||
                (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
                return Result.Failure("URL da foto deve ter esquema http ou https.");
        }
        ProfilePictureUrl = url;
        return Result.Success();
    }

    public bool VerifyPassword(string plainPassword) => Password.Verify(plainPassword);
}
