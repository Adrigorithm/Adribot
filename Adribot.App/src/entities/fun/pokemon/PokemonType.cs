using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class PokemonType
{
    // The order the Pokémon's types are listed in.
    [JsonPropertyName("slot")]
    public int Slot { get; set; }

    [JsonPropertyName("type")]
    public NamedApiResource Type { get; set; }
}
