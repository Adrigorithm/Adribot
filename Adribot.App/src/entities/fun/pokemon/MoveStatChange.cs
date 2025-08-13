using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record MoveStatChange(
    [property: JsonPropertyName("change")] int Change,
    [property: JsonPropertyName("stat")] NamedApiResource Stat
);
