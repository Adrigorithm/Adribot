using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record TypeRelationsPast(
    // The last generation in which the referenced type had the listed damage relations
    [property: JsonPropertyName("generation")] NamedApiResource Generation,

    // The damage relations the referenced type had up to and including the listed generation
    [property: JsonPropertyName("damage_relations")] TypeRelations DamageRelations
);
