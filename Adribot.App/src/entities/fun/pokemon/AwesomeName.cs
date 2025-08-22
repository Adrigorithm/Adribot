using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record AwesomeName(
    [property: JsonPropertyName("awesome_name")] string LocalisedAwesomeName,
    [property: JsonPropertyName("language")] NamedApiResource Language
);
