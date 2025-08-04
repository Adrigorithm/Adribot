using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record PokemonStat(
    [property: JsonPropertyName("stat")] NamedAPIResource Stat,
    [property: JsonPropertyName("effort")] int Effort,
    [property: JsonPropertyName("base_stat")] int BaseStat
);
