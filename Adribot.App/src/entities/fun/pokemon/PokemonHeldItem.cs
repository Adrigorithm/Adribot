using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class PokemonHeldItem
{
    [JsonPropertyName("item")]
    public NamedApiResource Item { get; set; }

    [JsonPropertyName("version_details")]
    public List<PokemonHeldItemVersion> VersionDetails { get; set; }
}
