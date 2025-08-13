using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record Genus(
    [property: JsonPropertyName("genus")] string LocalisedGenus,
    [property: JsonPropertyName("language")] NamedApiResource Language
);
