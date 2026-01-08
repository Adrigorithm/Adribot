using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class ItemAttribute
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("items")]
    public List<NamedApiResource> Items { get; set; }

    // The name of this item attribute listed in different languages.
    [JsonPropertyName("names")]
    public List<Name> Names { get; set; }

    // The description of this item attribute listed in different languages.
    [JsonPropertyName("descriptions")]
    public List<Description> Descriptions { get; set; }
}
