using Microsoft.Extensions.DependencyInjection;

using Bosqora.Heyzine.Clients;
using Bosqora.Heyzine.Clients.Interfaces;

namespace Bosqora.Heyzine.Extensions;

/// <summary>
/// Registers the Bosqora.Heyzine service wrappers and their shared HTTP client.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the Heyzine REST and management clients to the service collection.
    /// </summary>
    /// <param name="serviceCollection">The dependency injection container to update.</param>
    /// <returns>The same <see cref="IServiceCollection"/> instance so calls can be chained.</returns>
    /// <remarks>
    /// The registered wrappers expect <c>HeyzineClientId</c> for conversion endpoints and <c>HeyzineApiKey</c> for management endpoints to be available as environment variables.
    /// </remarks>
    public static IServiceCollection AddHeyzine(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddHttpClient(Constants.HTTPCLIENT_NAME, client =>
        {
            client.BaseAddress = new Uri(Constants.API_URL);
        }); 
        serviceCollection.AddTransient<IHeyzineRestClient, HeyzineRestClient>();
        serviceCollection.AddTransient<IHeyzineManagementClient, HeyzineManagementClient>();

        return serviceCollection;
    }
}