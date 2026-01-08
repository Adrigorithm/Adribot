using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class PokemonHeldItemVersion
{
    [JsonPropertyName("version")]
    public NamedApiResource Version { get; set; }

    [JsonPropertyName("rarity")]
    public int Rarity { get; set; }
}
