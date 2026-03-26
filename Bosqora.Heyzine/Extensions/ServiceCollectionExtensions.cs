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
        return AddHeyzine(serviceCollection, _ => { });
    }

    /// <summary>
    /// Adds the Heyzine REST and management clients to the service collection and allows shared HTTP client configuration.
    /// </summary>
    /// <param name="serviceCollection">The dependency injection container to update.</param>
    /// <param name="configureOptions">Applies wrapper-specific HTTP client settings such as a longer timeout for large synchronous conversions.</param>
    /// <returns>The same <see cref="IServiceCollection"/> instance so calls can be chained.</returns>
    /// <remarks>
    /// The registered wrappers expect <c>HeyzineClientId</c> for conversion endpoints and <c>HeyzineApiKey</c> for management endpoints to be available as environment variables.
    /// </remarks>
    public static IServiceCollection AddHeyzine(this IServiceCollection serviceCollection, Action<HeyzineClientOptions> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(serviceCollection, nameof(serviceCollection));
        ArgumentNullException.ThrowIfNull(configureOptions, nameof(configureOptions));

        var options = new HeyzineClientOptions();
        configureOptions(options);
        ValidateOptions(options);

        serviceCollection.AddHttpClient(Constants.HTTPCLIENT_NAME, client =>
        {
            client.BaseAddress = new Uri(Constants.API_URL);
            if (options.Timeout is not null)
            {
                client.Timeout = options.Timeout.Value;
            }
        }); 
        serviceCollection.AddTransient<IHeyzineRestClient, HeyzineRestClient>();
        serviceCollection.AddTransient<IHeyzineManagementClient, HeyzineManagementClient>();

        return serviceCollection;
    }

    private static void ValidateOptions(HeyzineClientOptions options)
    {
        if (options.Timeout is null)
        {
            return;
        }

        if (options.Timeout != Timeout.InfiniteTimeSpan && options.Timeout <= TimeSpan.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(options.Timeout), options.Timeout, "Timeout must be greater than zero or Timeout.InfiniteTimeSpan.");
        }
    }
}