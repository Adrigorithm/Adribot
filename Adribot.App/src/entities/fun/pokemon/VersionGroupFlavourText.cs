using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record VersionGroupFlavourText(
    [property: JsonPropertyName("text")] string Text,
    [property: JsonPropertyName("language")] NamedApiResource Language,

    // The version group which uses this flavour text.
    [property: JsonPropertyName("version_group")] NamedApiResource VersionGroup
);
