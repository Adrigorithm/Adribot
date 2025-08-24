using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record ContestName(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("color")] string Colour,
    [property: JsonPropertyName("language")] NamedApiResource Language
);
