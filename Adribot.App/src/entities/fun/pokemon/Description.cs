using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record Description(
    [property: JsonPropertyName("description")] string LocalisedDescription,
    [property: JsonPropertyName("language")] NamedApiResource Language
);
