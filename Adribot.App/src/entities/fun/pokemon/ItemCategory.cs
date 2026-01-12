using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.fun.pokemon;

public class ItemCategory
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("items")]
    public List<NamedApiResource> Items { get; set; }

    // The name of this item category listed in different languages.
    [JsonPropertyName("names")]
    public List<Name> Names { get; set; }

    // The pocket items in this category would be put in.
    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("pocket")]
    public NamedApiResource Pocket { get; set; }
}
