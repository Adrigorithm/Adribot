using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class Effect
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("effect")]
    public string LocalisedEffect { get; set; }

    [JsonPropertyName("language")]
    public NamedApiResource Language { get; set; }
}
