using FlurlApiClientExample.StoreApi;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddStoreApiClient();
        services.AddTransient<App>();
    })
    .Build();

var app = host.Services.GetRequiredService<App>();
await app.RunAsync();

// App.cs
public class App(StoreApiClient storeApiClient)
{
    public async Task RunAsync()
    {
        var products = await storeApiClient.ListProducts(new ProductFilterParams { CategoryId = 2, PriceMin = 20, PriceMax = 50 });

        Console.WriteLine("Products: ");
        foreach (var product in products)
        {
            Console.WriteLine(product.Title);
        }
    }
}