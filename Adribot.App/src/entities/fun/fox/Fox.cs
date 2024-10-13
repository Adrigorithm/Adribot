using System.Text.Json.Serialization;

namespace Adribot.Entities.Fun.Fox;

public record Fox(
    [property: JsonPropertyName("image")] string Image,
    [property: JsonPropertyName("link")] string Link
);
