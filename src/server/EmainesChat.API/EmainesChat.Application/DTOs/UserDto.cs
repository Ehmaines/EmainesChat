using EmainesChat.Domain.Aggregates.Users;
using EmainesChat.Domain.Enums;

namespace EmainesChat.Application.DTOs;

public record UserDto(Guid Id, string UserName, string? Name, string Email, string? ProfilePictureUrl, Roles Role)
{
    public static UserDto From(User user) =>
        new(user.Id, user.UserName, user.Name?.Value, user.Email.Value, user.ProfilePictureUrl, user.Role);
}
