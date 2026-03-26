using Microsoft.Extensions.DependencyInjection;

using Bosqora.Heyzine.Clients.Interfaces;
using Bosqora.Heyzine.Extensions;

namespace Bosqora.Heyzine.Tests.Extensions;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddHeyzine_ReturnsSameServiceCollection()
    {
        var services = new ServiceCollection();

        var result = services.AddHeyzine();

        Assert.Same(services, result);
    }

    [Fact]
    public void AddHeyzine_RegistersHeyzineRestClientAsTransient()
    {
        var services = new ServiceCollection();

        services.AddHeyzine();

        var descriptor = Assert.Single(services, sd =>
            sd.ServiceType == typeof(IHeyzineRestClient));
        Assert.Equal(ServiceLifetime.Transient, descriptor.Lifetime);
    }

    [Fact]
    public void AddHeyzine_RegistersHeyzineManagementClientAsTransient()
    {
        var services = new ServiceCollection();

        services.AddHeyzine();

        var descriptor = Assert.Single(services, sd =>
            sd.ServiceType == typeof(IHeyzineManagementClient));
        Assert.Equal(ServiceLifetime.Transient, descriptor.Lifetime);
    }

    [Fact]
    public void AddHeyzine_RegistersNamedHttpClient()
    {
        var services = new ServiceCollection();

        services.AddHeyzine();

        Assert.Contains(services, sd =>
            sd.ServiceType == typeof(IHttpClientFactory));
    }

    [Fact]
    public void AddHeyzine_ConfiguresHttpClientBaseAddress()
    {
        var services = new ServiceCollection();
        services.AddHeyzine();
        using var provider = services.BuildServiceProvider();
        var factory = provider.GetRequiredService<IHttpClientFactory>();

        using var client = factory.CreateClient("Bosqora.Heyzine.HttpClient");

        Assert.Equal(new Uri("https://heyzine.com/api1/"), client.BaseAddress);
    }

    [Fact]
    public void AddHeyzine_WithConfiguredTimeout_AppliesTimeoutToNamedHttpClient()
    {
        var services = new ServiceCollection();

        services.AddHeyzine(options =>
        {
            options.Timeout = TimeSpan.FromMinutes(10);
        });

        using var provider = services.BuildServiceProvider();
        var factory = provider.GetRequiredService<IHttpClientFactory>();

        using var client = factory.CreateClient(Constants.HTTPCLIENT_NAME);

        Assert.Equal(TimeSpan.FromMinutes(10), client.Timeout);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void AddHeyzine_WithNonPositiveTimeout_ThrowsArgumentOutOfRangeException(int seconds)
    {
        var services = new ServiceCollection();

        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => services.AddHeyzine(options =>
        {
            options.Timeout = TimeSpan.FromSeconds(seconds);
        }));

        Assert.Equal("Timeout", exception.ParamName);
    }
}
