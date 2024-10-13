using System.Text.Json.Serialization;

namespace Adribot.Entities.Fun.Dog;

public record Dog(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("url")] string Url,
    [property: JsonPropertyName("width")] int? Width,
    [property: JsonPropertyName("height")] int? Height
);
