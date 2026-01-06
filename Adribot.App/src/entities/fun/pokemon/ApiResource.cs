using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class ApiResource
{
    [JsonPropertyName("url")]
    public string Url { get; set; }
}
