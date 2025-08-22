using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record NaturePokeathlonStatAffect(
    // The maximum amount of change to the referenced Pokéathlon stat.
    [property: JsonPropertyName("max_change")] int MaxChange,

    // The nature causing the change.
    [property: JsonPropertyName("nature")] NamedApiResource Nature
);
