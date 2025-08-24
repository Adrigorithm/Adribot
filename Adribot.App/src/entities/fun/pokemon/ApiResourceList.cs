using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record ApiResourceList(
    [property: JsonPropertyName("count")] int Count,

    // The URL for the next page in the list.
    [property: JsonPropertyName("next")] string Next,

    // The URL for the previous page in the list.
    [property: JsonPropertyName("previous")] string Previous,

    [property: JsonPropertyName("results")] List<ApiResource> Results
);
