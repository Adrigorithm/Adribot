using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.fun.pokemon;

public class VersionEncounterDetail
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("version")]
    public NamedApiResource Version { get; set; }

    // The total percentage of all encounter potential.
    [JsonPropertyName("max_chance")]
    public int MaxChance { get; set; }

    [JsonPropertyName("encounter_details")]
    public List<Encounter> EncounterDetails { get; set; }
}
