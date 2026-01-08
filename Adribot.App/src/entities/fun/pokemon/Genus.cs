using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class Genus
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("genus")]
    public string LocalisedGenus { get; set; }

    [JsonPropertyName("language")]
    public NamedApiResource Language { get; set; }
}
