using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class AwesomeName
{
    [JsonPropertyName("awesome_name")]
    public string LocalisedAwesomeName { get; set; }

    [JsonPropertyName("language")]
    public NamedApiResource Language { get; set; }
}
