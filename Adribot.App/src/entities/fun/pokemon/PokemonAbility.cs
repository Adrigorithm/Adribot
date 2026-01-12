using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.fun.pokemon;

public class PokemonAbility
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("is_hidden")]
    public bool IsHidden { get; set; }

    // The slot this ability occupies in this Pokémon species.
    [JsonPropertyName("slot")]
    public int Slot { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("ability")]
    public NamedApiResource Ability { get; set; }
}
