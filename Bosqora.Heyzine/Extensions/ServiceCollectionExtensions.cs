using Microsoft.Extensions.DependencyInjection;

using Bosqora.Heyzine.Clients;
using Bosqora.Heyzine.Clients.Interfaces;

namespace Bosqora.Heyzine.Extensions;

public static class ServiceCollectionExtensions
{
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