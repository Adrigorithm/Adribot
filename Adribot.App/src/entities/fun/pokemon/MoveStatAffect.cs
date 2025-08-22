using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record MoveStatAffect(
    // The maximum amount of change to the referenced stat.
    [property: JsonPropertyName("change")] int Change,

    // The move causing the change.
    [property: JsonPropertyName("move")] NamedApiResource Move
);
