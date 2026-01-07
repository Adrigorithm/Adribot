using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class EncounterMethodRate
{
    [JsonPropertyName("encounter_method")]
    public NamedApiResource EncounterMethod { get; set; }

    // The chance of the encounter to occur on a version of the game.
    [JsonPropertyName("version_details")]
    public List<EncounterVersionDetails> VersionDetails { get; set; }
}
