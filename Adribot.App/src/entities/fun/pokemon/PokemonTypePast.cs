using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.fun.pokemon;

public class PokemonTypePast
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("generation")]
    public NamedApiResource Generation { get; set; }

    [JsonPropertyName("types")]
    public List<PokemonType> Types { get; set; }
}
