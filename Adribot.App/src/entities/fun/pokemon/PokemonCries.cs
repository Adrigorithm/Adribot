using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class PokemonCries
{
    [JsonPropertyName("latest")]
    public string Latest { get; set; }

    [JsonPropertyName("legacy")]
    public string Legacy { get; set; }
}
