using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record FlavourText(
    [property: JsonPropertyName("flavor_text")] string LocalisedFlavourText,
    [property: JsonPropertyName("language")] NamedApiResource Language,

    // The game version this flavour text is extracted from.
    [property: JsonPropertyName("version")] NamedApiResource Version
);
