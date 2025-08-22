using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record Nature(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("decreased_stat")] NamedApiResource DecreasedStat,
    [property: JsonPropertyName("increased_stat")] NamedApiResource IncreasedStat,
    [property: JsonPropertyName("hates_flavor")] NamedApiResource HatesFlavour,
    [property: JsonPropertyName("likes_flavor")] NamedApiResource LikesFlavour,

    // A list of Pokéathlon stats this nature effects and how much it effects them
    [property: JsonPropertyName("pokeathlon_stat_changes")] List<NatureStatChange> PokeathlonStatChanges,

    // A list of battle styles and how likely a Pokémon with this nature is to use them in the Battle Palace or Battle Tent.
    [property: JsonPropertyName("move_battle_style_preferences")] List<MoveBattleStylePreference> MoveBattleStylePreferences,

    // The name of this resource listed in different languages.
    [property: JsonPropertyName("names")] List<Name> Names
);
