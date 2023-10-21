using System.Text.Json.Serialization;

namespace Adribot.src.entities.fun.cat;

public record Weight(
    [property: JsonPropertyName("imperial")] string Imperial,
    [property: JsonPropertyName("metric")] string Metric
);