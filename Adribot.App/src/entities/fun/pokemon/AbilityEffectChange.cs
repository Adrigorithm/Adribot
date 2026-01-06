using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class AbilityEffectChange
{
    // The previous effect of this ability listed in different languages.
    [JsonPropertyName("effect_entries")]
    public List<Effect> EffectEntries { get; set; }

    [JsonPropertyName("version_group")]
    public NamedApiResource VersionGroup { get; set; }
}
