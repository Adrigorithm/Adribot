using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class TypePokemon
{
    // The order the Pokémon's types are listed in.
    [JsonPropertyName("slot")]
    public int Slot { get; set; }

    [JsonPropertyName("pokemon")]
    public NamedApiResource Pokemon { get; set; }
}
