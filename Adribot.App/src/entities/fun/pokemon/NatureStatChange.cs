using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record NatureStatChange(
    // The amount of change.
    [property: JsonPropertyName("max_change")] int MaxChange,

    // The stat being affected.
    [property: JsonPropertyName("pokeathlon_stat")] NamedApiResource PokeathlonStat
);
