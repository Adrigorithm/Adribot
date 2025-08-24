using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record EncounterMethodRate(
    [property: JsonPropertyName("encounter_method")] NamedApiResource EncounterMethod,

    // The chance of the encounter to occur on a version of the game.
    [property: JsonPropertyName("version_details")] List<EncounterVersionDetails> VersionDetails
);
