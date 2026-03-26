using System.Net;
using System.Net.Http;
using System.Text.Json;

using Moq;
using Moq.Protected;

using Bosqora.Heyzine.Clients;
using Bosqora.Heyzine.Models;
using Bosqora.Heyzine.Exceptions;

namespace Bosqora.Heyzine.Tests.Clients;


public class HeyzineRestClientTests
{
    readonly IHttpClientFactory mockHttpClientFactory;

    public HeyzineRestClientTests()
    {
        HeyzineResponse response = new()
        {
            Id = "success",
            Url = new Uri("https://heyzine.com/test"),
            Thumbnail = new Uri("https://heyzine.com/test/thumbnail"),
            Pdf = new Uri("https://heyzine.com/test.pdf"),
            Metadata = new HeyzineMetadata()
            {
                NumberOfPages = 10,
                AspectRatio = 1.5f
            }
        };
        mockHttpClientFactory = CreateHttpClientFactory(new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonSerializer.Serialize(response))
        });
    }

    static IHttpClientFactory CreateHttpClientFactory(HttpResponseMessage responseMessage)
    {
        var mockFactory = new Mock<IHttpClientFactory>();
        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);
        mockFactory.Setup(x => x.CreateClient(It.IsAny<string>()))
            .Returns(new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri(Constants.API_URL)
            });

        return mockFactory.Object;
    }

    [Fact]
    public void Constructor_WhenIHttpClientFactoryNull_ShouldThrow()
    {
        var exception = Assert.Throws<ArgumentNullException>(() => new HeyzineRestClient(null));

        Assert.Equal("httpClientFactory", exception.ParamName);
    }

    [Fact]
    public void Constructor_WhenClientIdNotSet_ShouldThrow()
    {
        Environment.SetEnvironmentVariable(Constants.CLIENTID_SETTINGNAME, null);

        var exception = Assert.Throws<EnvironmentVariableNotSetException>(() => new HeyzineRestClient(mockHttpClientFactory));

        Assert.Equal(string.Format(Constants.ERRORS_ENVVAR_NOTSET, Constants.CLIENTID_SETTINGNAME), exception.Message);
    }

    [Fact]
    public void Constructor_WhenClientIdSet_ShouldInitializeClient()
    {
        Environment.SetEnvironmentVariable(Constants.CLIENTID_SETTINGNAME, "testClientId");

        var client = new HeyzineRestClient(mockHttpClientFactory);

        Assert.NotNull(client);
        Assert.IsType<HeyzineRestClient>(client);
        Assert.NotNull(client.GetType().GetField("_httpClient", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance));
    }

    [Fact]
    public async Task ConvertPdfAsync_WhenParametersSet_ShouldReturnResponse()
    {
        Environment.SetEnvironmentVariable(Constants.CLIENTID_SETTINGNAME, "testClientId");

        var client = new HeyzineRestClient(mockHttpClientFactory);
        var pdfLocation = new Uri("https://example.com/test.pdf");

        var response = await client.ConvertPdfAsync(pdfLocation);

        Assert.NotNull(response);
        Assert.IsType<HeyzineResponse>(response);
    }

    [Fact]
    public async Task ConvertPdfAsync_WhenApiReturnsFailureStatus_ShouldThrowHttpRequestException()
    {
        Environment.SetEnvironmentVariable(Constants.CLIENTID_SETTINGNAME, "testClientId");
        var client = new HeyzineRestClient(CreateHttpClientFactory(new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.BadRequest,
            Content = new StringContent("bad request")
        }));
        var pdfLocation = new Uri("https://example.com/test.pdf");

        await Assert.ThrowsAsync<HttpRequestException>(() => client.ConvertPdfAsync(pdfLocation));
    }
}