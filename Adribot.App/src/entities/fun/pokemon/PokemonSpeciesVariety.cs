using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class PokemonSpeciesVariety
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    // Whether this variety is the default variety.
    [JsonPropertyName("is_default")]
    public bool IsDefault { get; set; }

    [JsonPropertyName("pokemon")]
    public NamedApiResource Pokemon { get; set; }
}
