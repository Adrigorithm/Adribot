using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.fun.pokemon;

public class PokemonAbilityPast
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("generation")]
    public NamedApiResource Generation { get; set; }

    [JsonPropertyName("abilities")]
    public List<PokemonAbility> Abilities { get; set; }
}
