namespace EmainesChat.Domain.ValueObjects;

public sealed class Password
{
    public string Hash { get; }

    private Password(string hash) => Hash = hash;

    public static Password CreateHashed(string plainText) => new(BCrypt.Net.BCrypt.HashPassword(plainText));

    public static Password FromHash(string hash) => new(hash);

    public bool Verify(string plainText) => BCrypt.Net.BCrypt.Verify(plainText, Hash);
}
