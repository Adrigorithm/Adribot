using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class Pokemon
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("base_experience")]
    public int BaseExperience { get; set; }

    // The height of this Pokémon in decimetres.
    [JsonPropertyName("height")]
    public int Height { get; set; }

    // Set for exactly one Pokémon used as the default for each species.
    [JsonPropertyName("is_default")]
    public bool IsDefault { get; set; }

    // Order for sorting. Almost national order, except families are grouped together.
    [JsonPropertyName("order")]
    public int Order { get; set; }

    // The weight of this Pokémon in hectograms.
    [JsonPropertyName("weight")]
    public int Weight { get; set; }

    [JsonPropertyName("abilities")]
    public List<PokemonAbility> Abilities { get; set; }

    [JsonPropertyName("forms")]
    public List<NamedApiResource> Forms { get; set; }

    // A list of game indices relevant to Pokémon item by generation.
    [JsonPropertyName("game_indices")]
    public List<VersionGameIndex> GameIndices { get; set; }

    [JsonPropertyName("held_items")]
    public List<PokemonHeldItem> HeldItems { get; set; }

    [JsonPropertyName("location_area_encounters")]
    public string LocationAreaEncounters { get; set; }

    [JsonPropertyName("moves")]
    public List<PokemonMove> Moves { get; set; }

    [JsonPropertyName("past_types")]
    public List<PokemonTypePast> PastTypes { get; set; }

    [JsonPropertyName("past_abilities")]
    public List<PokemonAbilityPast> PastAbilities { get; set; }

    // A set of sprites used to depict this Pokémon in the game.
    [JsonPropertyName("sprites")]
    public PokemonSprites Sprites { get; set; }

    // A set of cries used to depict this Pokémon in the game.
    [JsonPropertyName("cries")]
    public PokemonCries Cries { get; set; }

    [JsonPropertyName("species")]
    public List<NamedApiResource> Species { get; set; }

    [JsonPropertyName("stats")]
    public List<PokemonStat> Stats { get; set; }

    [JsonPropertyName("types")]
    public List<PokemonType> Types { get; set; }
}
