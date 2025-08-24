using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record ItemSprites(
    // The default depiction of this item
    [property: JsonPropertyName("default")] string Default
);
