using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class PokemonStat
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("stat")]
    public NamedApiResource Stat { get; set; }

    [JsonPropertyName("effort")]
    public int Effort { get; set; }

    [JsonPropertyName("base_stat")]
    public int BaseStat { get; set; }
}
