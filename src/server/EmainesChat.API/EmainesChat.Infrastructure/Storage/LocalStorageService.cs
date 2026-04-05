using EmainesChat.Application.Common;
using Microsoft.AspNetCore.Http;

namespace EmainesChat.Infrastructure.Storage;

/// <summary>
/// Armazena arquivos no diretório wwwroot/pictures/ do servidor.
/// Adequado apenas para desenvolvimento local — não use em produção:
///   - arquivos são perdidos ao recriar o container Docker;
///   - incompatível com múltiplas instâncias da API (scale-out).
/// Para produção, registre uma implementação baseada em Azure Blob Storage, AWS S3, etc.
/// </summary>
public class LocalStorageService : IStorageService
{
    private const string PicturesFolder = "pictures";
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LocalStorageService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> SaveAsync(Stream stream, string fileName, string contentType, CancellationToken ct = default)
    {
        var directory = Path.Combine("wwwroot", PicturesFolder);
        Directory.CreateDirectory(directory);

        var filePath = Path.Combine(directory, fileName);
        await using var fileStream = new FileStream(filePath, FileMode.Create);
        await stream.CopyToAsync(fileStream, ct);

        var request = _httpContextAccessor.HttpContext!.Request;
        return $"{request.Scheme}://{request.Host}/{PicturesFolder}/{fileName}";
    }

    public Task DeleteAsync(string fileUrl, CancellationToken ct = default)
    {
        var fileName = Path.GetFileName(new Uri(fileUrl).LocalPath);
        var filePath = Path.Combine("wwwroot", PicturesFolder, fileName);
        if (File.Exists(filePath))
            File.Delete(filePath);
        return Task.CompletedTask;
    }
}
