using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record NamedApiResource(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("url")] string Url
);
