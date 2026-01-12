using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.fun.pokemon;

public class MoveStatAffect
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    // The maximum amount of change to the referenced stat.
    [JsonPropertyName("change")]
    public int Change { get; set; }

    // The move causing the change.
    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("move")]
    public NamedApiResource Move { get; set; }
}
