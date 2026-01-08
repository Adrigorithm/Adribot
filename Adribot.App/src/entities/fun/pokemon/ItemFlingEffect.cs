using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class ItemFlingEffect
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    // The result of this fling effect listed in different languages.
    [JsonPropertyName("effect_entries")]
    public List<Effect> EffectEntries { get; set; }

    [JsonPropertyName("items")]
    public List<NamedApiResource> Items { get; set; }
}
