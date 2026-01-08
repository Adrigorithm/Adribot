using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class ItemSprites
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    // The default depiction of this item.
    [JsonPropertyName("default")]
    public string Default { get; set; }
}
