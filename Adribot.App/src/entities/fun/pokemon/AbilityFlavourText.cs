using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class AbilityFlavourText
{
    // The localized name for an API resource in a specific language.
    [JsonPropertyName("flavor_text")]
    public string FlavourText { get; set; }

    [JsonPropertyName("language")]
    public NamedApiResource Language { get; set; }

    [JsonPropertyName("version_group")]
    public NamedApiResource VersionGroup { get; set; }
}
