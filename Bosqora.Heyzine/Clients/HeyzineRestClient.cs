using System.Net.Http.Json;

using Bosqora.Heyzine.Clients.Interfaces;
using Bosqora.Heyzine.Exceptions;
using Bosqora.Heyzine.Models;

namespace Bosqora.Heyzine.Clients;

/// <summary>
/// Default implementation of <see cref="IHeyzineRestClient"/> that wraps the Heyzine conversion and oEmbed endpoints.
/// </summary>
/// <remarks>
/// The client reads the conversion client id from the <c>HeyzineClientId</c> environment variable and injects it into conversion requests.
/// </remarks>
public sealed class HeyzineRestClient : IHeyzineRestClient
{
    private readonly HttpClient _httpClient;
    private readonly string _clientId;

    /// <summary>
    /// Initializes a new instance of the <see cref="HeyzineRestClient"/> class.
    /// </summary>
    /// <param name="httpClientFactory">The factory used to create the configured Heyzine <see cref="HttpClient"/> instance.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="httpClientFactory"/> is <see langword="null"/>.</exception>
    /// <exception cref="EnvironmentVariableNotSetException">Thrown when the <c>HeyzineClientId</c> environment variable is missing or empty.</exception>
    public HeyzineRestClient(IHttpClientFactory httpClientFactory)
    {
        ArgumentNullException.ThrowIfNull(httpClientFactory);
        var clientId = Environment.GetEnvironmentVariable(Constants.CLIENTID_SETTINGNAME);
        if (string.IsNullOrWhiteSpace(clientId))
        {
            throw new EnvironmentVariableNotSetException(Constants.CLIENTID_SETTINGNAME);
        }
        _clientId = clientId;
        _httpClient = httpClientFactory.CreateClient(Constants.HTTPCLIENT_NAME);
    }

    /// <inheritdoc />
    public Task<HeyzineResponse?> ConvertPdfAsync(Uri pdfLocation, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(pdfLocation);

        return ConvertPdfAsync(new HeyzineConversionRequest
        {
            Pdf = pdfLocation,
        }, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<HeyzineResponse?> ConvertPdfAsync(HeyzineConversionRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var response = await _httpClient.PostAsJsonAsync(Constants.API_REST_POSTFIX, CreateAuthenticatedRequest(request), cancellationToken);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<HeyzineResponse?>(cancellationToken: cancellationToken);
    }

    /// <inheritdoc />
    public async Task<HeyzineOEmbedResponse?> GetOEmbedAsync(Uri flipbookUrl, int? maxWidth = null, int? maxHeight = null, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(flipbookUrl);

        List<string> query =
        [
            $"url={Uri.EscapeDataString(flipbookUrl.AbsoluteUri)}",
            "format=json"
        ];

        if (maxWidth is not null)
        {
            query.Add($"maxwidth={maxWidth.Value}");
        }

        if (maxHeight is not null)
        {
            query.Add($"maxheight={maxHeight.Value}");
        }

        var response = await _httpClient.GetAsync($"{Constants.OEMBED_POSTFIX}?{string.Join("&", query)}", cancellationToken);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<HeyzineOEmbedResponse?>(cancellationToken: cancellationToken);
    }

    /// <inheritdoc />
    public async Task<HeyzineResponse?> StartPdfConversionAsync(HeyzineConversionRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var response = await _httpClient.PostAsJsonAsync(Constants.API_ASYNC_POSTFIX, CreateAuthenticatedRequest(request), cancellationToken);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<HeyzineResponse?>(cancellationToken: cancellationToken);
    }

    private HeyzineConversionRequest CreateAuthenticatedRequest(HeyzineConversionRequest request)
    {
        ArgumentNullException.ThrowIfNull(request.Pdf, nameof(request.Pdf));

        return new HeyzineConversionRequest
        {
            BackgroundColor = request.BackgroundColor,
            ClientId = _clientId,
            Description = request.Description,
            Download = request.Download,
            FullScreen = request.FullScreen,
            Logo = request.Logo,
            Pdf = request.Pdf,
            PreviousNext = request.PreviousNext,
            PrivateNote = request.PrivateNote,
            Share = request.Share,
            ShowInfo = request.ShowInfo,
            Subtitle = request.Subtitle,
            Tags = request.Tags,
            Template = request.Template,
            Title = request.Title,
        };
    }
}
