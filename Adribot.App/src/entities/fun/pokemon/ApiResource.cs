using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record ApiResource(
    [property: JsonPropertyName("url")] string Url
);
