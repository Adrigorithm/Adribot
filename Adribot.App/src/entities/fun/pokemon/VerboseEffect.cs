using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class VerboseEffect
{
    [JsonPropertyName("effect")]
    public string Effect { get; set; }

    [JsonPropertyName("short_effect")]
    public string ShortEffect { get; set; }

    [JsonPropertyName("language")]
    public NamedApiResource Language { get; set; }
}
