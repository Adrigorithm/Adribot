using System.Text.Json.Serialization;

namespace Adribot.src.entities.fun.fox;

public record Fox(
    [property: JsonPropertyName("image")] string Image,
    [property: JsonPropertyName("link")] string Link
);