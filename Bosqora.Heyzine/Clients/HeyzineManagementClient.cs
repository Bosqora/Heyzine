using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

using Bosqora.Heyzine.Clients.Interfaces;
using Bosqora.Heyzine.Exceptions;
using Bosqora.Heyzine.Models;

namespace Bosqora.Heyzine.Clients;

/// <summary>
/// Default implementation of <see cref="IHeyzineManagementClient"/> that wraps the authenticated Heyzine management endpoints.
/// </summary>
/// <remarks>
/// The client reads the API key from the <c>HeyzineApiKey</c> environment variable and sends it as a bearer token.
/// </remarks>
public class HeyzineManagementClient : IHeyzineManagementClient
{
    #region Fields

    private readonly HttpClient _httpClient;

#endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="HeyzineManagementClient"/> class.
    /// </summary>
    /// <param name="httpClientFactory">The factory used to create the configured Heyzine <see cref="HttpClient"/> instance.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="httpClientFactory"/> is <see langword="null"/>.</exception>
    /// <exception cref="EnvironmentVariableNotSetException">Thrown when the <c>HeyzineApiKey</c> environment variable is missing or empty.</exception>
    public HeyzineManagementClient(IHttpClientFactory httpClientFactory)
    {
        ArgumentNullException.ThrowIfNull(httpClientFactory, nameof(httpClientFactory));

        var apiKey = Environment.GetEnvironmentVariable(Constants.APIKEY_SETTINGNAME);
        if (string.IsNullOrEmpty(apiKey))
        {
            throw new EnvironmentVariableNotSetException(Constants.APIKEY_SETTINGNAME);
        }

        _httpClient = httpClientFactory.CreateClient(Constants.HTTPCLIENT_NAME);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
    }

    #endregion

    /// <inheritdoc />
    public Task<HeyzineApiResult?> AddFlipbookToBookshelfAsync(HeyzineBookshelfFlipbookRequest request, CancellationToken cancellationToken = default)
    {
        return PostAsync<HeyzineBookshelfFlipbookRequest, HeyzineApiResult>(Constants.BOOKSHELF_ADD_POSTFIX, request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<HeyzineApiResult?> AddUserAccessAsync(HeyzineAccessEntryRequest request, CancellationToken cancellationToken = default)
    {
        return PostAsync<HeyzineAccessEntryRequest, HeyzineApiResult>(Constants.ACCESS_ADD_POSTFIX, request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<HeyzineApiResult?> ConfigurePasswordProtectionAsync(HeyzineAccessSetupRequest request, CancellationToken cancellationToken = default)
    {
        return PostAsync<HeyzineAccessSetupRequest, HeyzineApiResult>(Constants.ACCESS_SETUP_POSTFIX, request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<HeyzineApiResult?> DeleteFlipbookAsync(string flipbookId, CancellationToken cancellationToken = default)
    {
        return PostAsync<DeleteFlipbookRequest, HeyzineApiResult>(Constants.FLIPBOOK_DELETE_POSTFIX, new DeleteFlipbookRequest
        {
            Id = ValidateIdentifier(flipbookId, nameof(flipbookId))
        }, cancellationToken);
    }

    /// <inheritdoc />
    public Task<HeyzineFlipbook?> GetFlipbookDetailsAsync(string flipbookId, CancellationToken cancellationToken = default)
    {
        var validatedId = ValidateIdentifier(flipbookId, nameof(flipbookId));
        return GetAsync<HeyzineFlipbook>($"{Constants.FLIPBOOK_DETAILS_POSTFIX}?id={Uri.EscapeDataString(validatedId)}", cancellationToken);
    }

    /// <inheritdoc />
    public Task<IReadOnlyList<HeyzineBookshelf>?> ListBookshelvesAsync(CancellationToken cancellationToken = default)
    {
        return GetAsync<IReadOnlyList<HeyzineBookshelf>>(Constants.BOOKSHELF_LIST_POSTFIX, cancellationToken);
    }

    /// <inheritdoc />
    public Task<IReadOnlyList<HeyzineFlipbook>?> ListBookshelfFlipbooksAsync(string bookshelfId, CancellationToken cancellationToken = default)
    {
        var validatedId = ValidateIdentifier(bookshelfId, nameof(bookshelfId));
        return GetAsync<IReadOnlyList<HeyzineFlipbook>>($"{Constants.BOOKSHELF_FLIPBOOKS_POSTFIX}?id={Uri.EscapeDataString(validatedId)}", cancellationToken);
    }

    /// <inheritdoc />
    public Task<IReadOnlyList<HeyzineFlipbook>?> ListFlipbooksAsync(CancellationToken cancellationToken = default)
    {
        return GetAsync<IReadOnlyList<HeyzineFlipbook>>(Constants.FLIPBOOK_LIST_POSTFIX, cancellationToken);
    }

    /// <inheritdoc />
    public Task<HeyzineApiResult?> RemoveFlipbookFromBookshelfAsync(HeyzineBookshelfFlipbookRequest request, CancellationToken cancellationToken = default)
    {
        return PostAsync<HeyzineBookshelfFlipbookRequest, HeyzineApiResult>(Constants.BOOKSHELF_REMOVE_POSTFIX, request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<HeyzineApiResult?> RemoveUserAccessAsync(HeyzineAccessRemovalRequest request, CancellationToken cancellationToken = default)
    {
        return PostAsync<HeyzineAccessRemovalRequest, HeyzineApiResult>(Constants.ACCESS_REMOVE_POSTFIX, request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<HeyzineApiResult?> SetBookshelfSocialMetadataAsync(HeyzineSocialMetadataRequest request, CancellationToken cancellationToken = default)
    {
        return PostAsync<HeyzineSocialMetadataRequest, HeyzineApiResult>(Constants.BOOKSHELF_SOCIAL_POSTFIX, request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<HeyzineApiResult?> SetFlipbookSocialMetadataAsync(HeyzineSocialMetadataRequest request, CancellationToken cancellationToken = default)
    {
        return PostAsync<HeyzineSocialMetadataRequest, HeyzineApiResult>(Constants.FLIPBOOK_SOCIAL_POSTFIX, request, cancellationToken);
    }

    private async Task<TResponse?> GetAsync<TResponse>(string requestUri, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync(requestUri, cancellationToken);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<TResponse?>(cancellationToken: cancellationToken);
    }

    private async Task<TResponse?> PostAsync<TRequest, TResponse>(string requestUri, TRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var response = await _httpClient.PostAsJsonAsync(requestUri, request, cancellationToken);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<TResponse?>(cancellationToken: cancellationToken);
    }

    private static string ValidateIdentifier(string identifier, string parameterName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(identifier, parameterName);
        return identifier;
    }

    private sealed class DeleteFlipbookRequest
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
    }
}