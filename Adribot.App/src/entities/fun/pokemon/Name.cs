using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record Name(
    [property: JsonPropertyName("name")] string LocalisedName,
    [property: JsonPropertyName("language")] NamedApiResource Language
);
