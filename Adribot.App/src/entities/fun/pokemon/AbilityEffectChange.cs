using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record AbilityEffectChange(
    // The previous effect of this ability listed in different languages.
    [property: JsonPropertyName("effect_entries")] List<Effect> EffectEntries,

    [property: JsonPropertyName("version_group")] NamedApiResource VersionGroup
);
