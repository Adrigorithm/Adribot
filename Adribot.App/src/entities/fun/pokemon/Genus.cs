using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record Genus(
    [property: JsonPropertyName("genus")] string LocalisedGenus,
    [property: JsonPropertyName("language")] NamedApiResource Language
);
