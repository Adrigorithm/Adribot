using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.fun.pokemon;

public class Type
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    // A detail of how effective this type is toward others and vice versa.
    [JsonPropertyName("damage_relations")]
    public TypeRelations DamageRelations { get; set; }

    // A list of details of how effective this type was toward others and vice versa in previous generations
    [JsonPropertyName("past_damage_relations")]
    public List<TypeRelationsPast> PastDamageRelations { get; set; }

    // A list of game indices relevant to this item by generation.
    [JsonPropertyName("game_indices")]
    public List<GenerationGameIndex> GameIndices { get; set; }

    // The generation this type was introduced in.
    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("generation")]
    public NamedApiResource Generation { get; set; }

    // The class of damage inflicted by this type.
    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("move_damage_class")]
    public NamedApiResource MoveDamageClass { get; set; }

    // The name of this resource listed in different languages.
    [JsonPropertyName("names")]
    public List<Name> Names { get; set; }

    // A list of details of Pokémon that have this type.
    [JsonPropertyName("pokemon")]
    public List<TypePokemon> Pokemon { get; set; }

    // A list of moves that have this type.
    [JsonPropertyName("moves")]
    public List<NamedApiResource> Moves { get; set; }
}
