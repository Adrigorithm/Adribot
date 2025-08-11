using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record PokemonEncounter(
    [property: JsonPropertyName("pokemon")] NamedApiResource Pokemon,
    
    // A list of versions and encounters with Pokémon that might happen in the referenced location area.
    [property: JsonPropertyName("version_details")] List<VersionEncounterDetail> VersionDetails
);
