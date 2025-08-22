using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record ChainLink(
    [property: JsonPropertyName("is_baby")] bool IsBaby,
    [property: JsonPropertyName("species")] NamedApiResource Species,
    [property: JsonPropertyName("evolution_details")] List<EvolutionDetail> EvolutionDetails,
    [property: JsonPropertyName("evolves_to")] List<ChainLink> EvolvesTo
);
