using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class PastMoveStatValues
{
    // The percent value of how likely this move is to be successful.
    [JsonPropertyName("accuracy")]
    public int Accuracy { get; set; }

    // The percent value of how likely it is this moves effect will take effect.
    [JsonPropertyName("effect_chance")]
    public int EffectChance { get; set; }

    [JsonPropertyName("power")]
    public int Power { get; set; }

    [JsonPropertyName("PP")]
    public int Pp { get; set; }

    // The effect of this move listed in different languages.
    [JsonPropertyName("effect_entries")]
    public List<VerboseEffect> EffectEntries { get; set; }

    // The elemental type of this move.
    [JsonPropertyName("type")]
    public NamedApiResource Type { get; set; }

    // The version group in which these move stat values were in effect.
    [JsonPropertyName("version_group")]
    public NamedApiResource VersionGroup { get; set; }
}
