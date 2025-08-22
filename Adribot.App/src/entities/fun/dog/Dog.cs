using System.Text.Json.Serialization;

namespace Adribot.entities.fun.dog;

public record Dog(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("url")] string Url,
    [property: JsonPropertyName("width")] int? Width,
    [property: JsonPropertyName("height")] int? Height
);
