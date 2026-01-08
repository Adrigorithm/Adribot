using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class ItemSprites
{
    // The default depiction of this item.
    [JsonPropertyName("default")]
    public string Default { get; set; }
}
