using System.Net;
using System.Net.Http;
using System.Text.Json;

using Bosqora.Heyzine.Clients;
using Bosqora.Heyzine.Exceptions;
using Bosqora.Heyzine.Models;

namespace Bosqora.Heyzine.Tests.Clients;

public class HeyzineManagementClientTests
{
    [Fact]
    public void Constructor_WhenApiKeyNotSet_ShouldThrow()
    {
        Environment.SetEnvironmentVariable(Constants.APIKEY_SETTINGNAME, null);

        var exception = Assert.Throws<EnvironmentVariableNotSetException>(() => new HeyzineManagementClient(CreateHttpClientFactory()));

        Assert.Equal(string.Format(Constants.ERRORS_ENVVAR_NOTSET, Constants.APIKEY_SETTINGNAME), exception.Message);
    }

    [Fact]
    public async Task GetFlipbookDetailsAsync_WhenCalled_ShouldUseBearerAuthorization()
    {
        Environment.SetEnvironmentVariable(Constants.APIKEY_SETTINGNAME, "testApiKey");

        HttpRequestMessage? capturedRequest = null;
        var client = new HeyzineManagementClient(CreateHttpClientFactory(request =>
        {
            capturedRequest = request;
            return JsonResponse(new HeyzineFlipbook
            {
                Id = "flipbook-id"
            });
        }));

        var response = await client.GetFlipbookDetailsAsync("flipbook-id");

        Assert.NotNull(response);
        Assert.Equal(HttpMethod.Get, capturedRequest?.Method);
        Assert.Equal("Bearer", capturedRequest?.Headers.Authorization?.Scheme);
        Assert.Equal("testApiKey", capturedRequest?.Headers.Authorization?.Parameter);
        Assert.Equal("https://heyzine.com/api1/flipbook-details?id=flipbook-id", capturedRequest?.RequestUri?.AbsoluteUri);
    }

    [Fact]
    public async Task DeleteFlipbookAsync_WhenCalled_ShouldPostIdentifierPayload()
    {
        Environment.SetEnvironmentVariable(Constants.APIKEY_SETTINGNAME, "testApiKey");

        HttpRequestMessage? capturedRequest = null;
        var client = new HeyzineManagementClient(CreateHttpClientFactory(request =>
        {
            capturedRequest = request;
            return JsonResponse(new HeyzineApiResult
            {
                Success = true,
                Code = 200,
                Msg = "Flipbook deleted"
            });
        }));

        var response = await client.DeleteFlipbookAsync("flipbook-id");

        Assert.True(response!.Success);
        Assert.Equal(new Uri("https://heyzine.com/api1/flipbook-delete"), capturedRequest?.RequestUri);
        Assert.Equal(HttpMethod.Post, capturedRequest?.Method);

        using var payload = JsonDocument.Parse(await capturedRequest!.Content!.ReadAsStringAsync());
        Assert.Equal("flipbook-id", payload.RootElement.GetProperty("id").GetString());
    }

    [Fact]
    public async Task ListFlipbooksAsync_WhenCalled_ShouldReturnCollection()
    {
        Environment.SetEnvironmentVariable(Constants.APIKEY_SETTINGNAME, "testApiKey");

        var client = new HeyzineManagementClient(CreateHttpClientFactory(_ => JsonResponse(new[]
        {
            new HeyzineFlipbook { Id = "one" },
            new HeyzineFlipbook { Id = "two" }
        })));

        var response = await client.ListFlipbooksAsync();

        Assert.NotNull(response);
        Assert.Equal(2, response!.Count);
    }

    private static IHttpClientFactory CreateHttpClientFactory(Func<HttpRequestMessage, HttpResponseMessage>? responseFactory = null)
    {
        return new StubHttpClientFactory(new StubHttpMessageHandler(responseFactory ?? (_ => JsonResponse(new HeyzineApiResult { Success = true }))));
    }

    private static HttpResponseMessage JsonResponse<T>(T payload)
    {
        return new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonSerializer.Serialize(payload))
        };
    }

    private sealed class StubHttpClientFactory(HttpMessageHandler handler) : IHttpClientFactory
    {
        public HttpClient CreateClient(string name)
        {
            return new HttpClient(handler)
            {
                BaseAddress = new Uri(Constants.API_URL)
            };
        }
    }

    private sealed class StubHttpMessageHandler(Func<HttpRequestMessage, HttpResponseMessage> responseFactory) : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(responseFactory(request));
        }
    }
}