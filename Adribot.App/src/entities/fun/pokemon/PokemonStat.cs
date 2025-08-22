using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record PokemonStat(
    [property: JsonPropertyName("stat")] NamedApiResource Stat,
    [property: JsonPropertyName("effort")] int Effort,
    [property: JsonPropertyName("base_stat")] int BaseStat
);
