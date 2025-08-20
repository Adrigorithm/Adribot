using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record VersionEncounterDetail(
    [property: JsonPropertyName("version")] NamedApiResource Version,

    // The total percentage of all encounter potential.
    [property: JsonPropertyName("max_chance")] int MaxChance,
    [property: JsonPropertyName("encounter_details")] List<Encounter> EncounterDetails
);
