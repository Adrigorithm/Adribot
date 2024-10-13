using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.Fun.Cat;

public record Cat(
    [property: JsonPropertyName("breeds")] IReadOnlyList<Breed> Breeds,
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("url")] string Url,
    [property: JsonPropertyName("width")] int? Width,
    [property: JsonPropertyName("height")] int? Height
);
