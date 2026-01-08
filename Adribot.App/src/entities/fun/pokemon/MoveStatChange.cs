using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class MoveStatChange
{
    [JsonPropertyName("change")]
    public int Change { get; set; }

    [JsonPropertyName("stat")]
    public NamedApiResource Stat { get; set; }
}
