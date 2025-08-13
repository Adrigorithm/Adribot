using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record PastMoveStatValues(
    // The percent value of how likely this move is to be successful.
    [property: JsonPropertyName("accuracy")] int Accuracy,
    
    // The percent value of how likely it is this moves effect will take effect.
    [property: JsonPropertyName("effect_chance")] int EffectChance,
    
    [property: JsonPropertyName("power")] int Power,
    [property: JsonPropertyName("PP")] int Pp,
    
    // The effect of this move listed in different languages.
    [property: JsonPropertyName("effect_entries")] List<VerboseEffect> EffectEntries,
    
    // The elemental type of this move.
    [property: JsonPropertyName("type")] NamedApiResource Type,
    
    // The version group in which these move stat values were in effect.
    [property: JsonPropertyName("version_group")] NamedApiResource VersionGroup
);
