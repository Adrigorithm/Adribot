using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class AbilityPokemon
{
    [JsonPropertyName("is_hidden")]
    public bool IsHidden { get; set; }

    // Pokémon have 3 ability 'slots' which hold references to possible abilities they could have.
    // This is the slot of this ability for the referenced pokemon.
    [JsonPropertyName("slot")]
    public int Slot { get; set; }

    [JsonPropertyName("pokemon")]
    public NamedApiResource Pokemon { get; set; }
}
