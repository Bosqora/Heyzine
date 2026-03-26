using System.Net.Http.Headers;
using System.Net.Http.Json;

using Bosqora.Heyzine.Clients.Interfaces;
using Bosqora.Heyzine.Exceptions;
using Bosqora.Heyzine.Models;

namespace Bosqora.Heyzine.Clients;

public class HeyzineManagementClient : IHeyzineManagementClient
{
    private readonly HttpClient _httpClient;

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

    public Task<HeyzineApiResult?> AddFlipbookToBookshelfAsync(HeyzineBookshelfFlipbookRequest request, CancellationToken cancellationToken = default)
    {
        return PostAsync<HeyzineBookshelfFlipbookRequest, HeyzineApiResult>(Constants.BOOKSHELF_ADD_POSTFIX, request, cancellationToken);
    }

    public Task<HeyzineApiResult?> AddUserAccessAsync(HeyzineAccessEntryRequest request, CancellationToken cancellationToken = default)
    {
        return PostAsync<HeyzineAccessEntryRequest, HeyzineApiResult>(Constants.ACCESS_ADD_POSTFIX, request, cancellationToken);
    }

    public Task<HeyzineApiResult?> ConfigurePasswordProtectionAsync(HeyzineAccessSetupRequest request, CancellationToken cancellationToken = default)
    {
        return PostAsync<HeyzineAccessSetupRequest, HeyzineApiResult>(Constants.ACCESS_SETUP_POSTFIX, request, cancellationToken);
    }

    public Task<HeyzineApiResult?> DeleteFlipbookAsync(string flipbookId, CancellationToken cancellationToken = default)
    {
        return PostAsync<HeyzineIdentifierRequest, HeyzineApiResult>(Constants.FLIPBOOK_DELETE_POSTFIX, new HeyzineIdentifierRequest
        {
            Id = ValidateIdentifier(flipbookId, nameof(flipbookId))
        }, cancellationToken);
    }

    public Task<HeyzineFlipbook?> GetFlipbookDetailsAsync(string flipbookId, CancellationToken cancellationToken = default)
    {
        var validatedId = ValidateIdentifier(flipbookId, nameof(flipbookId));
        return GetAsync<HeyzineFlipbook>($"{Constants.FLIPBOOK_DETAILS_POSTFIX}?id={Uri.EscapeDataString(validatedId)}", cancellationToken);
    }

    public Task<IReadOnlyList<HeyzineBookshelf>?> ListBookshelvesAsync(CancellationToken cancellationToken = default)
    {
        return GetAsync<IReadOnlyList<HeyzineBookshelf>>(Constants.BOOKSHELF_LIST_POSTFIX, cancellationToken);
    }

    public Task<IReadOnlyList<HeyzineFlipbook>?> ListBookshelfFlipbooksAsync(string bookshelfId, CancellationToken cancellationToken = default)
    {
        var validatedId = ValidateIdentifier(bookshelfId, nameof(bookshelfId));
        return GetAsync<IReadOnlyList<HeyzineFlipbook>>($"{Constants.BOOKSHELF_FLIPBOOKS_POSTFIX}?id={Uri.EscapeDataString(validatedId)}", cancellationToken);
    }

    public Task<IReadOnlyList<HeyzineFlipbook>?> ListFlipbooksAsync(CancellationToken cancellationToken = default)
    {
        return GetAsync<IReadOnlyList<HeyzineFlipbook>>(Constants.FLIPBOOK_LIST_POSTFIX, cancellationToken);
    }

    public Task<HeyzineApiResult?> RemoveFlipbookFromBookshelfAsync(HeyzineBookshelfFlipbookRequest request, CancellationToken cancellationToken = default)
    {
        return PostAsync<HeyzineBookshelfFlipbookRequest, HeyzineApiResult>(Constants.BOOKSHELF_REMOVE_POSTFIX, request, cancellationToken);
    }

    public Task<HeyzineApiResult?> RemoveUserAccessAsync(HeyzineAccessRemovalRequest request, CancellationToken cancellationToken = default)
    {
        return PostAsync<HeyzineAccessRemovalRequest, HeyzineApiResult>(Constants.ACCESS_REMOVE_POSTFIX, request, cancellationToken);
    }

    public Task<HeyzineApiResult?> SetBookshelfSocialMetadataAsync(HeyzineSocialMetadataRequest request, CancellationToken cancellationToken = default)
    {
        return PostAsync<HeyzineSocialMetadataRequest, HeyzineApiResult>(Constants.BOOKSHELF_SOCIAL_POSTFIX, request, cancellationToken);
    }

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
}