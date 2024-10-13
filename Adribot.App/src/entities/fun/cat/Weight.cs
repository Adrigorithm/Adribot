using System.Text.Json.Serialization;

namespace Adribot.Entities.Fun.Cat;

public record Weight(
    [property: JsonPropertyName("imperial")] string Imperial,
    [property: JsonPropertyName("metric")] string Metric
);
