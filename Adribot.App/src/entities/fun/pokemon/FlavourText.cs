using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class FlavourText
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("flavor_text")]
    public string LocalisedFlavourText { get; set; }

    [JsonPropertyName("language")]
    public NamedApiResource Language { get; set; }

    // The game version this flavour text is extracted from.
    [JsonPropertyName("version")]
    public NamedApiResource Version { get; set; }
}
