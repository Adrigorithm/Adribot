using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record Type(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,

    // A detail of how effective this type is toward others and vice versa.
    [property: JsonPropertyName("damage_relations")] TypeRelations DamageRelations,
    
    // A list of details of how effective this type was toward others and vice versa in previous generations
    [property: JsonPropertyName("past_damage_relations")] List<TypeRelationsPast> PastDamageRelations,
    
    // A list of game indices relevant to this item by generation.
    [property: JsonPropertyName("game_indices")] List<GenerationGameIndex> GameIndices,

    // The generation this type was introduced in.
    [property: JsonPropertyName("generation")] NamedApiResource Generation,

    // The class of damage inflicted by this type.
    [property: JsonPropertyName("move_damage_class")] NamedApiResource MoveDamageClass,
    
    // The name of this resource listed in different languages.
    [property: JsonPropertyName("names")] List<Name> Names,
    
    // A list of details of Pokémon that have this type.
    [property: JsonPropertyName("pokemon")] List<TypePokemon> Pokemon,

    // A list of moves that have this type.
    [property: JsonPropertyName("moves")] List<NamedApiResource> Moves
);
