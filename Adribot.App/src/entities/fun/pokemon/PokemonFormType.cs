using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class PokemonFormType
{
    // The order the Pokémon's types are listed in.
    [JsonPropertyName("slot")]
    public int Slot { get; set; }

    [JsonPropertyName("type")]
    public NamedApiResource Type { get; set; }
}
