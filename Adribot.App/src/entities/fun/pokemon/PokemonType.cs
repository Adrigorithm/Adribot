using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.fun.pokemon;

public class PokemonType
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    // The order the Pokémon's types are listed in.
    [JsonPropertyName("slot")]
    public int Slot { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("type")]
    public NamedApiResource Type { get; set; }
}
