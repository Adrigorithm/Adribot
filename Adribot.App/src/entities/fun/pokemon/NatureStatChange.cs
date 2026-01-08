using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class NatureStatChange
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    // The amount of change.
    [JsonPropertyName("max_change")]
    public int MaxChange { get; set; }

    // The stat being affected.
    [JsonPropertyName("pokeathlon_stat")]
    public NamedApiResource PokeathlonStat { get; set; }
}
