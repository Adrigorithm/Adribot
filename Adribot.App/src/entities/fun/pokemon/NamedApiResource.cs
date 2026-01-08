using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class NamedApiResource
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }
}
