using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record NamedApiResource(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("url")] string Url
);
