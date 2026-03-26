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
}
