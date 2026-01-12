using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.fun.pokemon;

public class PokemonHeldItemVersion
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("version")]
    public NamedApiResource Version { get; set; }

    [JsonPropertyName("rarity")]
    public int Rarity { get; set; }
}
