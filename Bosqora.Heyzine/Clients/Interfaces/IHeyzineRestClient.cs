using Bosqora.Heyzine.Models;

namespace Bosqora.Heyzine.Clients.Interfaces;

public interface IHeyzineRestClient
{
    Task<HeyzineResponse?> ConvertPdfAsync(Uri pdfLocation, CancellationToken cancellationToken = default);

    Task<HeyzineResponse?> ConvertPdfAsync(HeyzineConversionRequest request, CancellationToken cancellationToken = default);

    Task<HeyzineOEmbedResponse?> GetOEmbedAsync(Uri flipbookUrl, int? maxWidth = null, int? maxHeight = null, CancellationToken cancellationToken = default);

    Task<HeyzineResponse?> StartPdfConversionAsync(HeyzineConversionRequest request, CancellationToken cancellationToken = default);
}