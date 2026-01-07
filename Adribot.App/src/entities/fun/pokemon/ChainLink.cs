using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class ChainLink
{
    [JsonPropertyName("is_baby")]
    public bool IsBaby { get; set; }

    [JsonPropertyName("species")]
    public NamedApiResource Species { get; set; }

    [JsonPropertyName("evolution_details")]
    public List<EvolutionDetail> EvolutionDetails { get; set; }

    [JsonPropertyName("evolves_to")]
    public List<ChainLink> EvolvesTo { get; set; }
}
