using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class ApiResourceList
{
    [JsonPropertyName("count")]
    public int Count { get; set; }

    // The URL for the next page in the list.
    [JsonPropertyName("next")]
    public string Next { get; set; }

    // The URL for the previous page in the list.
    [JsonPropertyName("previous")]
    public string Previous { get; set; }

    [JsonPropertyName("results")]
    public List<ApiResource> Results { get; set; }
}
