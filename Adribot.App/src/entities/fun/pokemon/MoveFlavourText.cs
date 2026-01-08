using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class MoveFlavourText
{
    [JsonPropertyName("flavor_text")]
    public string FlavourText { get; set; }

    [JsonPropertyName("language")]
    public NamedApiResource Language { get; set; }

    // The version group that uses this flavor text.
    [JsonPropertyName("version_group")]
    public NamedApiResource VersionGroup { get; set; }
}
