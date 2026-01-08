using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class MoveStatAffect
{
    // The maximum amount of change to the referenced stat.
    [JsonPropertyName("change")]
    public int Change { get; set; }

    // The move causing the change.
    [JsonPropertyName("move")]
    public NamedApiResource Move { get; set; }
}
