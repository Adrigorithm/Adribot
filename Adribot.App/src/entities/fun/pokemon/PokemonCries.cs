using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class PokemonCries
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("latest")]
    public string Latest { get; set; }

    [JsonPropertyName("legacy")]
    public string Legacy { get; set; }
}
