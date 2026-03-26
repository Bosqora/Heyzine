using System.Net;
using System.Net.Http;
using System.Text.Json;

using Bosqora.Heyzine.Clients;
using Bosqora.Heyzine.Exceptions;
using Bosqora.Heyzine.Models;

namespace Bosqora.Heyzine.Tests.Clients;


public class HeyzineRestClientTests
{
    [Fact]
    public void Constructor_WhenIHttpClientFactoryNull_ShouldThrow()
    {
        var exception = Assert.Throws<ArgumentNullException>(() => new HeyzineRestClient(null!));

        Assert.Equal("httpClientFactory", exception.ParamName);
    }

    [Fact]
    public void Constructor_WhenClientIdNotSet_ShouldThrow()
    {
        Environment.SetEnvironmentVariable(Constants.CLIENTID_SETTINGNAME, null);

        var exception = Assert.Throws<EnvironmentVariableNotSetException>(() => new HeyzineRestClient(CreateHttpClientFactory()));

        Assert.Equal(string.Format(Constants.ERRORS_ENVVAR_NOTSET, Constants.CLIENTID_SETTINGNAME), exception.Message);
    }

    [Fact]
    public async Task ConvertPdfAsync_WhenParametersSet_ShouldReturnResponse()
    {
        Environment.SetEnvironmentVariable(Constants.CLIENTID_SETTINGNAME, "testClientId");

        HttpRequestMessage? capturedRequest = null;
        var client = new HeyzineRestClient(CreateHttpClientFactory(request =>
        {
            capturedRequest = request;
            return JsonResponse(new HeyzineResponse
            {
                Id = "success",
                Url = new Uri("https://heyzine.com/test"),
                Thumbnail = new Uri("https://heyzine.com/test/thumbnail"),
                Pdf = new Uri("https://heyzine.com/test.pdf"),
                Metadata = new HeyzineMetadata
                {
                    NumberOfPages = 10,
                    AspectRatio = 1.5f
                }
            });
        }));

        var response = await client.ConvertPdfAsync(new Uri("https://example.com/test.pdf"));

        Assert.NotNull(response);
        Assert.Equal(HttpMethod.Post, capturedRequest?.Method);
        Assert.Equal(new Uri("https://heyzine.com/api1/rest"), capturedRequest?.RequestUri);

        using var payload = JsonDocument.Parse(await capturedRequest!.Content!.ReadAsStringAsync());
        Assert.Equal("https://example.com/test.pdf", payload.RootElement.GetProperty("pdf").GetString());
        Assert.Equal("testClientId", payload.RootElement.GetProperty("client_id").GetString());
    }

    [Fact]
    public async Task StartPdfConversionAsync_WhenCalled_ShouldUseAsyncEndpoint()
    {
        Environment.SetEnvironmentVariable(Constants.CLIENTID_SETTINGNAME, "testClientId");

        HttpRequestMessage? capturedRequest = null;
        var client = new HeyzineRestClient(CreateHttpClientFactory(request =>
        {
            capturedRequest = request;
            return JsonResponse(new HeyzineResponse
            {
                Id = "success",
                State = "started"
            });
        }));

        var response = await client.StartPdfConversionAsync(new HeyzineConversionRequest
        {
            Pdf = new Uri("https://example.com/test.pdf"),
            ShowInfo = true,
        });

        Assert.NotNull(response);
        Assert.Equal("started", response!.State);
        Assert.Equal(new Uri("https://heyzine.com/api1/async"), capturedRequest?.RequestUri);
    }

    [Fact]
    public async Task GetOEmbedAsync_WhenCalled_ShouldUseQueryString()
    {
        Environment.SetEnvironmentVariable(Constants.CLIENTID_SETTINGNAME, "testClientId");

        HttpRequestMessage? capturedRequest = null;
        var client = new HeyzineRestClient(CreateHttpClientFactory(request =>
        {
            capturedRequest = request;
            return JsonResponse(new HeyzineOEmbedResponse
            {
                Title = "Sample"
            });
        }));

        var response = await client.GetOEmbedAsync(new Uri("https://heyzine.com/flip-book/sample.html"), maxWidth: 800, maxHeight: 600);

        Assert.NotNull(response);
        Assert.Equal(HttpMethod.Get, capturedRequest?.Method);
        Assert.Equal("https://heyzine.com/api1/oembed?url=https%3A%2F%2Fheyzine.com%2Fflip-book%2Fsample.html&format=json&maxwidth=800&maxheight=600", capturedRequest?.RequestUri?.AbsoluteUri);
    }

    [Fact]
    public async Task ConvertPdfAsync_WhenApiReturnsFailureStatus_ShouldThrowHttpRequestException()
    {
        Environment.SetEnvironmentVariable(Constants.CLIENTID_SETTINGNAME, "testClientId");
        var client = new HeyzineRestClient(CreateHttpClientFactory(_ => new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.BadRequest,
            Content = new StringContent("bad request")
        }));

        await Assert.ThrowsAsync<HttpRequestException>(() => client.ConvertPdfAsync(new Uri("https://example.com/test.pdf")));
    }

    private static IHttpClientFactory CreateHttpClientFactory(Func<HttpRequestMessage, HttpResponseMessage>? responseFactory = null)
    {
        return new StubHttpClientFactory(new StubHttpMessageHandler(responseFactory ?? (_ => JsonResponse(new HeyzineResponse()))));
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