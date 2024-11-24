using Microsoft.Extensions.DependencyInjection;

namespace FlurlApiClientExample.StoreApi;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddStoreApiClient(this IServiceCollection services)
    {
        // Register the API client
        services.AddHttpClient<StoreApiClient>(client => client.BaseAddress = new Uri("https://api.escuelajs.co/api/v1/ "));
        
        return services;
    }
}