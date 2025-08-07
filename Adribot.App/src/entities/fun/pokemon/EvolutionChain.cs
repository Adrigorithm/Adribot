using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record EvolutionChain(
    [property: JsonPropertyName("id")] int Id,
    
    // The item that a Pokémon would be holding when mating that would trigger the egg hatching a baby Pokémon rather than a basic Pokémon.
    [property: JsonPropertyName("baby_trigger_item")] NamedApiResource BabyTriggerItem,
    
    // The base chain link object. Each link contains evolution details for a Pokémon in the chain. Each link references the next Pokémon in the natural evolution order.
    [property: JsonPropertyName("chain")] ChainLink Chain
);
