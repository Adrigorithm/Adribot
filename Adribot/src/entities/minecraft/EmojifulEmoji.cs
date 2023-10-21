using System.Text.Json.Serialization;

namespace Adribot.src.entities.minecraft;

public record EmojifulEmoji
{
    [JsonPropertyName("category")]
    public string Category { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }
}