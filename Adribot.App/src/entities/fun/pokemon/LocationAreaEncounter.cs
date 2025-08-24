using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record LocationAreaEncounter(
    [property: JsonPropertyName("location_area")] NamedApiResource LocationArea,

    // A list of versions and encounters with the referenced Pokémon that might happen.
    [property: JsonPropertyName("version_details")] List<VersionEncounterDetail> VersionDetails
);
