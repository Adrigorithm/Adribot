using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record ItemFlingEffect(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    
    // The result of this fling effect listed in different languages.
    [property: JsonPropertyName("effect_entries")] List<Effect> EffectEntries,
    
    [property: JsonPropertyName("items")] List<NamedApiResource> Items
);
