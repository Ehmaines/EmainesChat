using EmainesChat.Domain.Common;
using EmainesChat.Domain.Enums;
using EmainesChat.Domain.ValueObjects;

namespace EmainesChat.Domain.Aggregates.Users;

public class User
{
    public int Id { get; private set; }
    public string UserName { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public Password Password { get; private set; } = null!;
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

        return Result.Success(new User
        {
            UserName = userName,
            Email = emailResult.Value,
            Password = Password.CreateHashed(plainPassword),
            Role = Roles.User,
            CreatedAt = DateTime.UtcNow
        });
    }

    public bool VerifyPassword(string plainPassword) => Password.Verify(plainPassword);
}
