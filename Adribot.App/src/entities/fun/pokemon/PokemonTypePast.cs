using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class PokemonTypePast
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("generation")]
    public NamedApiResource Generation { get; set; }

    [JsonPropertyName("types")]
    public List<PokemonType> Types { get; set; }
}
