using EmainesChat.Application.Common;
using EmainesChat.Application.Users.Commands.RegisterUser;
using EmainesChat.Application.Users.Commands.UpdateUserPicture;
using EmainesChat.Application.Users.Commands.UpdateUserProfile;
using EmainesChat.Application.Users.Queries.GetAllUsers;
using EmainesChat.Application.Users.Queries.GetUserById;
using EmainesChat.Application.Users.Queries.GetUserProfile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmainesChat.API.Controllers;

public record UpdateProfileRequest(
    string? Name,
    string? Email,
    string? CurrentPassword,
    string? NewPassword
);

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IStorageService _storage;

    public UserController(ISender sender, IStorageService storage)
    {
        _sender = sender;
        _storage = storage;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await _sender.Send(new GetAllUsersQuery());
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var result = await _sender.Send(new GetUserByIdQuery(id));
        return result.IsSuccess ? Ok(result.Value) : NotFound(new { message = result.Error });
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetProfile()
    {
        var userId = Guid.Parse(User.FindFirst("Id")!.Value);
        var result = await _sender.Send(new GetUserProfileQuery(userId));
        return result.IsSuccess ? Ok(result.Value) : NotFound(new { message = result.Error });
    }

    [HttpPatch("me")]
    [Authorize]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequest request)
    {
        var userId = Guid.Parse(User.FindFirst("Id")!.Value);
        var command = new UpdateUserProfileCommand(
            userId,
            request.Name,
            request.Email,
            request.CurrentPassword,
            request.NewPassword
        );
        var result = await _sender.Send(command);
        return result.IsSuccess ? Ok(result.Value) : UnprocessableEntity(new { message = result.Error });
    }

    [HttpPost("me/picture")]
    [Authorize]
    public async Task<IActionResult> UploadPicture(IFormFile file, CancellationToken ct)
    {
        var allowedTypes = new[] { "image/jpeg", "image/png", "image/webp" };
        if (!allowedTypes.Contains(file.ContentType))
            return UnprocessableEntity(new { message = "Formato de arquivo não permitido. Use jpg, jpeg, png ou webp." });

        if (file.Length > 5 * 1024 * 1024)
            return UnprocessableEntity(new { message = "O arquivo excede o tamanho máximo de 5 MB." });

        var userId = Guid.Parse(User.FindFirst("Id")!.Value);

        // Deleta o arquivo antigo antes de salvar o novo
        var profileResult = await _sender.Send(new GetUserProfileQuery(userId), ct);
        if (profileResult.IsSuccess && profileResult.Value.ProfilePictureUrl is not null)
            await _storage.DeleteAsync(profileResult.Value.ProfilePictureUrl, ct);

        var extension = file.ContentType switch
        {
            "image/jpeg" => ".jpg",
            "image/png"  => ".png",
            "image/webp" => ".webp",
            _ => ".jpg"
        };
        // GUID garante URL única a cada upload, evitando cache do browser
        var fileName = $"{userId}_{Guid.NewGuid():N}{extension}";

        await using var stream = file.OpenReadStream();
        var url = await _storage.SaveAsync(stream, fileName, file.ContentType, ct);

        var result = await _sender.Send(new UpdateUserPictureCommand(userId, url), ct);
        return result.IsSuccess ? Ok(new { profilePictureUrl = url }) : BadRequest(new { message = result.Error });
    }

    [HttpDelete("me/picture")]
    [Authorize]
    public async Task<IActionResult> RemovePicture(CancellationToken ct)
    {
        var userId = Guid.Parse(User.FindFirst("Id")!.Value);

        var profileResult = await _sender.Send(new GetUserProfileQuery(userId), ct);
        if (profileResult.IsSuccess && profileResult.Value.ProfilePictureUrl is not null)
            await _storage.DeleteAsync(profileResult.Value.ProfilePictureUrl, ct);

        var result = await _sender.Send(new UpdateUserPictureCommand(userId, null), ct);
        return result.IsSuccess ? Ok(new { profilePictureUrl = (string?)null }) : BadRequest(new { message = result.Error });
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand command)
    {
        var result = await _sender.Send(command);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(new { message = result.Error });
    }
}
