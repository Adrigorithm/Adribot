using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class ContestName
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("color")]
    public string Colour { get; set; }

    [JsonPropertyName("language")]
    public NamedApiResource Language { get; set; }
}
