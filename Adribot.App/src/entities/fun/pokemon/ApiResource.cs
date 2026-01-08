using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class ApiResource
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }
}
