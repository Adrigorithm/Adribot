using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record ContestName(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("color")] string Colour,
    [property: JsonPropertyName("language")] NamedApiResource Language
);
