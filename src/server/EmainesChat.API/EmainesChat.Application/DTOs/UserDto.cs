using EmainesChat.Domain.Aggregates.Users;
using EmainesChat.Domain.Enums;

namespace EmainesChat.Application.DTOs;

public record UserDto(int Id, string UserName, string Email, Roles Role)
{
    public static UserDto From(User user) =>
        new(user.Id, user.UserName, user.Email.Value, user.Role);
}
