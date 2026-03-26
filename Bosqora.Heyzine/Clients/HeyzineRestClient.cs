using System.Net.Http.Json;

using Bosqora.Heyzine.Clients.Interfaces;
using Bosqora.Heyzine.Exceptions;
using Bosqora.Heyzine.Models;

namespace Bosqora.Heyzine.Clients;

public class HeyzineRestClient : IHeyzineRestClient
{
    #region Private fields

    private readonly HttpClient _httpClient;
    private readonly string _clientId;

    #endregion

    #region Constructors

    public HeyzineRestClient(IHttpClientFactory httpClientFactory)
    {
        ArgumentNullException.ThrowIfNull(httpClientFactory, nameof(httpClientFactory));
        var clientId = Environment.GetEnvironmentVariable(Constants.CLIENTID_SETTINGNAME);
        if (string.IsNullOrEmpty(clientId))
        { 
            throw new EnvironmentVariableNotSetException(Constants.CLIENTID_SETTINGNAME);
        }
        _clientId = clientId;
        _httpClient = httpClientFactory.CreateClient(Constants.HTTPCLIENT_NAME);
    }

    #endregion

    public async Task<HeyzineResponse?> ConvertPdfAsync(Uri pdfLocation)
    {
        HeyzineRequest request = new(pdfLocation, _clientId);
        var response = await _httpClient.PostAsJsonAsync(Constants.API_REST_POSTFIX, request);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<HeyzineResponse?>();
    }
}
