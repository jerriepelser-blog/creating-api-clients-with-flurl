using System.Text.Json;
using System.Text.Json.Serialization;
using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.Extensions.Logging;

namespace FlurlApiClientExample.StoreApi;

public class StoreApiClient
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
    };

    private readonly FlurlClient _flurlClient;

    public StoreApiClient(HttpClient httpClient, ILogger<StoreApiClient> logger)
    {
        _flurlClient = new FlurlClient(httpClient)
            .WithHeader("User-Agent", UserAgentGenerator.Generate())
            .WithSettings(settings => { settings.JsonSerializer = new DefaultJsonSerializer(JsonSerializerOptions); });
        _flurlClient.OnError(async call =>
        {
            if (call.Response != null)
            {
                var responseText = await call.Response.GetStringAsync();
                logger.LogWarning(call.Exception,
                    "Status code {StatusCode} received when calling the Store API. Response text: {ResponseText}",
                    call.Response.StatusCode,
                    responseText);
            }
        });
    }

    public Task<ProductModel[]> ListProducts(ProductFilterParams? filter = null)
    {
        return _flurlClient.Request("products")
            .SetQueryParams(new
            {
                categoryId = filter?.CategoryId,
                price_min = filter?.PriceMin,
                price_max = filter?.PriceMax,
            })
            .GetJsonAsync<ProductModel[]>();
    }
}
