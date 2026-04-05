namespace EmainesChat.Application.Common;

/// <summary>
/// Abstração para armazenamento de arquivos.
/// A implementação padrão (<see cref="LocalStorageService"/>) salva em disco local (wwwroot/pictures/).
/// Para produção, substitua por uma implementação que use Azure Blob Storage, AWS S3, etc.
/// A troca é feita apenas no registro de DI em <c>EmainesChat.Infrastructure/DependencyInjection.cs</c> —
/// nenhum outro código precisa mudar.
/// </summary>
public interface IStorageService
{
    /// <summary>
    /// Persiste um arquivo e retorna a URL pública para acesso.
    /// </summary>
    Task<string> SaveAsync(Stream stream, string fileName, string contentType, CancellationToken ct = default);

    /// <summary>
    /// Remove o arquivo associado à URL pública informada.
    /// Implementações que não suportam remoção síncrona podem ignorar silenciosamente.
    /// </summary>
    Task DeleteAsync(string fileUrl, CancellationToken ct = default);
}
