using System.Text.Json.Serialization;

namespace FlurlApiClientExample.StoreApi;

public class ProductFilterParams
{
    public int? CategoryId { get; set; }
    public int? PriceMax { get; set; }
    public int? PriceMin { get; set; }
}

public record ProductModel(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("price")] decimal Price,
    [property: JsonPropertyName("description")] string Description,
    [property: JsonPropertyName("images")] string[] Images,
    [property: JsonPropertyName("creationAt")] DateTime CreationAt,
    [property: JsonPropertyName("updatedAt")] DateTime UpdatedAt,
    [property: JsonPropertyName("category")] CategoryModel Category
);

public record CategoryModel(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("image")] string Image,
    [property: JsonPropertyName("creationAt")] DateTime CreationAt,
    [property: JsonPropertyName("updatedAt")] DateTime UpdatedAt
);