using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class VersionGroupFlavourText
{
    [JsonPropertyName("text")]
    public string Text { get; set; }

    [JsonPropertyName("language")]
    public NamedApiResource Language { get; set; }

    // The version group which uses this flavour text.
    [JsonPropertyName("version_group")]
    public NamedApiResource VersionGroup { get; set; }
}
