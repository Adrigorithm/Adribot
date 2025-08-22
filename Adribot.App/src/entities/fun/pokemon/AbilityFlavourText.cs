using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record AbilityFlavourText(
    // The localized name for an API resource in a specific language.
    [property: JsonPropertyName("flavor_text")] string FlavourText,
    [property: JsonPropertyName("language")] NamedApiResource Language,
    [property: JsonPropertyName("version_group")] NamedApiResource VersionGroup
);
