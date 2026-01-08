using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class PokemonMove
{
    [JsonPropertyName("move")]
    public NamedApiResource Move { get; set; }

    [JsonPropertyName("version_group_details")]
    public List<PokemonMoveVersion> VersionGroupDetails { get; set; }
}
