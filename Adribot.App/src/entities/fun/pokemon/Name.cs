using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class Name
{
    [JsonPropertyName("name")]
    public string LocalisedName { get; set; }

    [JsonPropertyName("language")]
    public NamedApiResource Language { get; set; }
}
