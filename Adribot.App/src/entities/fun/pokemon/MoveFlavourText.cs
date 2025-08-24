using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record MoveFlavourText(
    [property: JsonPropertyName("flavor_text")] string FlavourText,
    [property: JsonPropertyName("language")] NamedApiResource Language,

    // The version group that uses this flavor text.
    [property: JsonPropertyName("version_group")] NamedApiResource VersionGroup
);
