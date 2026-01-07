using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class Effect
{
    [JsonPropertyName("effect")]
    public string LocalisedEffect { get; set; }

    [JsonPropertyName("language")]
    public NamedApiResource Language { get; set; }
}
