using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.fun.pokemon;

public class LocationAreaEncounter
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("location_area")]
    public NamedApiResource LocationArea { get; set; }

    // A list of versions and encounters with the referenced Pokémon that might happen.
    [JsonPropertyName("version_details")]
    public List<VersionEncounterDetail> VersionDetails { get; set; }
}
