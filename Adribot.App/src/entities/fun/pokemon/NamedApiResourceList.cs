using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class NamedApiResourceList
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("count")]
    public int Count { get; set; }

    // The URL for the next page in the list.
    [JsonPropertyName("next")]
    public string Next { get; set; }

    // The URL for the previous page in the list.
    [JsonPropertyName("previous")]
    public string Previous { get; set; }

    [JsonPropertyName("results")]
    public List<NamedApiResource> Results { get; set; }
}
