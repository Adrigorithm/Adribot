using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class PokemonEncounter
{
    [JsonPropertyName("pokemon")]
    public NamedApiResource Pokemon { get; set; }

    // A list of versions and encounters with Pokémon that might happen in the referenced location area.
    [JsonPropertyName("version_details")]
    public List<VersionEncounterDetail> VersionDetails { get; set; }
}
