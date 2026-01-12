using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.fun.pokemon;

public class EncounterMethodRate
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("encounter_method")]
    public NamedApiResource EncounterMethod { get; set; }

    // The chance of the encounter to occur on a version of the game.
    [JsonPropertyName("version_details")]
    public List<EncounterVersionDetails> VersionDetails { get; set; }
}
