using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class Description
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("description")]
    public string LocalisedDescription { get; set; }

    [JsonPropertyName("language")]
    public NamedApiResource Language { get; set; }
}
