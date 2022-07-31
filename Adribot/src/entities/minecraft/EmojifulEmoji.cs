using System.Text.Json;
using System.Text.Json.Serialization;

public class EmojifulEmoji
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