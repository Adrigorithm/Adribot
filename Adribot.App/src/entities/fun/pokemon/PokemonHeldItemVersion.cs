using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class PokemonHeldItemVersion
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("version")]
    public NamedApiResource Version { get; set; }

    [JsonPropertyName("rarity")]
    public int Rarity { get; set; }
}
