using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record ApiResource(
    [property: JsonPropertyName("url")] string Url
);
