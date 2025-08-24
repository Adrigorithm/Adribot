using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record MoveStatChange(
    [property: JsonPropertyName("change")] int Change,
    [property: JsonPropertyName("stat")] NamedApiResource Stat
);
