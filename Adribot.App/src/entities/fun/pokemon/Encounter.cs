using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.fun.pokemon;

public class Encounter
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    // The lowest level the Pokémon could be encountered at.
    [JsonPropertyName("min_level")]
    public int MinLevel { get; set; }

    // The highest level the Pokémon could be encountered at.
    [JsonPropertyName("max_level")]
    public int MaxLevel { get; set; }

    // A list of condition values that must be in effect for this encounter to occur.
    [JsonPropertyName("condition_values")]
    public List<NamedApiResource> ConditionValues { get; set; }

    // Percent chance that this encounter will occur.
    [JsonPropertyName("chance")]
    public int Chance { get; set; }

    // The method by which this encounter happens.
    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("method")]
    public NamedApiResource Method { get; set; }
}
