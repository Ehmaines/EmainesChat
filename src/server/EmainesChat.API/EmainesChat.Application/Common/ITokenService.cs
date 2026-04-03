using EmainesChat.Domain.Aggregates.Users;

namespace EmainesChat.Application.Common;

public interface ITokenService
{
    string GenerateToken(User user);
}
