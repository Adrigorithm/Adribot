using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.fun.pokemon;

public class BerryFlavour
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("berries")]
    public List<FlavourBerryMap> Berries { get; set; }

    // The contest type that correlates with this berry flavour.
    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("contest_type")]
    public NamedApiResource ContestType { get; set; }

    // The name of this resource listed in different languages.
    [JsonPropertyName("names")]
    public List<Name> Names { get; set; }
}
