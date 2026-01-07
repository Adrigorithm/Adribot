using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class EvolutionChain
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    // The item that a Pokémon would be holding when mating that would trigger the egg hatching a baby Pokémon rather than a basic Pokémon.
    [JsonPropertyName("baby_trigger_item")]
    public NamedApiResource BabyTriggerItem { get; set; }

    // The base chain link object. Each link contains evolution details for a Pokémon in the chain. Each link references the next Pokémon in the natural evolution order.
    [JsonPropertyName("chain")]
    public ChainLink Chain { get; set; }
}
