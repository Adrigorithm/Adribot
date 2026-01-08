using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class PokemonAbility
{
    [JsonPropertyName("is_hidden")]
    public bool IsHidden { get; set; }

    // The slot this ability occupies in this Pokémon species.
    [JsonPropertyName("slot")]
    public int Slot { get; set; }

    [JsonPropertyName("ability")]
    public NamedApiResource Ability { get; set; }
}
