using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record Stat(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,

    // ID the games use for this stat.
    [property: JsonPropertyName("game_index")] int GameIndex,
    
    // Whether this stat only exists within a battle.
    [property: JsonPropertyName("is_battle_only")] bool IsBattleOnly,
    
    // A detail of moves which affect this stat positively or negatively.
    [property: JsonPropertyName("affecting_moves")] MoveStatAffectSets AffectingMoves,
    
    // A detail of natures which affect this stat positively or negatively.
    [property: JsonPropertyName("affecting_natures")] NatureStatAffectSets AffectingNatures,
    
    // A list of characteristics that are set on a Pokémon when its highest base stat is this stat.
    [property: JsonPropertyName("characteristics")] List<ApiResource> Characteristics,
    
    // The class of damage this stat is directly related to.
    [property: JsonPropertyName("move_damage_class")] NamedApiResource MoveDamageClass,
    
    // The name of this resource listed in different languages.
    [property: JsonPropertyName("names")] List<Name> Names
);
