using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record Name(
    [property: JsonPropertyName("name")] string LocalisedName,
    [property: JsonPropertyName("language")] NamedApiResource Language
);
