using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record Pokemon(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("base_experience")] int  BaseExperience,
    
    // The height of this Pokémon in decimetres.
    [property: JsonPropertyName("height")] int Height,
    
    // Set for exactly one Pokémon used as the default for each species.
    [property: JsonPropertyName("is_default")] bool IsDefault,
    
    // Order for sorting. Almost national order, except families are grouped together.
    [property: JsonPropertyName("order")] int Order,
    
    // The weight of this Pokémon in hectograms.
    [property: JsonPropertyName("weight")] int Weight,
    
    [property: JsonPropertyName("abilities")] List<PokemonAbility> Abilities,
    
    [property: JsonPropertyName("forms")] List<NamedApiResource> Forms,
    
    // A list of game indices relevant to Pokémon item by generation.
    [property: JsonPropertyName("game_indices")] List<VersionGameIndex> GameIndices,
    
    [property: JsonPropertyName("held_items")] List<PokemonHeldItem> HeldItems,
    [property: JsonPropertyName("location_area_encounters")] string LocationAreaEncounters,
    [property: JsonPropertyName("moves")] List<PokemonMove> Moves,
    [property: JsonPropertyName("past_types")] List<PokemonTypePast> PastTypes,
    [property: JsonPropertyName("past_abilities")] List<PokemonAbilityPast> PastAbilities,
    
    // A set of sprites used to depict this Pokémon in the game. A visual representation of the various sprites can be found at PokeAPI/sprites
    [property: JsonPropertyName("sprites")] PokemonSprites Sprites,
    
    // A set of cries used to depict this Pokémon in the game. A visual representation of the various cries can be found at PokeAPI/cries
    [property: JsonPropertyName("cries")] PokemonCries Cries,
    
    [property: JsonPropertyName("species")] List<NamedApiResource> Species,
    [property: JsonPropertyName("stats")] List<PokemonStat> Stats,
    [property: JsonPropertyName("types")] List<PokemonType> Types
);
