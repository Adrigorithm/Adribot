using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record ItemSprites(
    // The default depiction of this item
    [property: JsonPropertyName("default")] string Default
);
