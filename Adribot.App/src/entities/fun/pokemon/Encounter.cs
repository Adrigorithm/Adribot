using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record Encounter(
    // The lowest level the Pokémon could be encountered at.
    [property: JsonPropertyName("min_level")] int MinLevel,
    
    // The highest level the Pokémon could be encountered at.
    [property: JsonPropertyName("max_level")] int MaxLevel,
    
    // A list of condition values that must be in effect for this encounter to occur.
    [property: JsonPropertyName("condition_values")] List<NamedApiResource> ConditionValues,
    
    // Percent chance that this encounter will occur.
    [property: JsonPropertyName("chance")] int Chance,
    
    // The method by which this encounter happens.
    [property: JsonPropertyName("method")] NamedApiResource Method
);
