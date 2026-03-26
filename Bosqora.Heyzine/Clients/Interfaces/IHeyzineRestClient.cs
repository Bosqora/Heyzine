using Bosqora.Heyzine.Models;

namespace Bosqora.Heyzine.Clients.Interfaces;

/// <summary>
/// Defines the conversion and oEmbed operations exposed by the Heyzine REST API.
/// </summary>
/// <remarks>
/// These operations require the <c>HeyzineClientId</c> environment variable to be configured on the concrete client.
/// </remarks>
public interface IHeyzineRestClient
{
    /// <summary>
    /// Converts a document to a flipbook using only the source document URL.
    /// </summary>
    /// <param name="pdfLocation">The direct URL of the PDF, DOCX, or PPTX document to convert.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the request.</param>
    /// <returns>The completed conversion response returned by the sync REST endpoint.</returns>
    Task<HeyzineResponse?> ConvertPdfAsync(Uri pdfLocation, CancellationToken cancellationToken = default);

    /// <summary>
    /// Converts a document to a flipbook using the full conversion payload supported by Heyzine.
    /// </summary>
    /// <param name="request">The conversion request to send to the sync REST endpoint.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the request.</param>
    /// <returns>The completed conversion response returned by the sync REST endpoint.</returns>
    Task<HeyzineResponse?> ConvertPdfAsync(HeyzineConversionRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the oEmbed payload for an existing flipbook URL.
    /// </summary>
    /// <param name="flipbookUrl">The public flipbook URL to query.</param>
    /// <param name="maxWidth">An optional maximum embed width passed through to the oEmbed endpoint.</param>
    /// <param name="maxHeight">An optional maximum embed height passed through to the oEmbed endpoint.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the request.</param>
    /// <returns>The oEmbed response returned by Heyzine.</returns>
    Task<HeyzineOEmbedResponse?> GetOEmbedAsync(Uri flipbookUrl, int? maxWidth = null, int? maxHeight = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Starts an asynchronous document conversion and returns the current conversion state immediately.
    /// </summary>
    /// <param name="request">The conversion request to send to the async endpoint.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the request.</param>
    /// <returns>The current conversion response, including the async processing state.</returns>
    Task<HeyzineResponse?> StartPdfConversionAsync(HeyzineConversionRequest request, CancellationToken cancellationToken = default);
}