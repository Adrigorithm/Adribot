using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record Move(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("accuracy")] int Accuracy,

    // The percent value of how likely it is this moves effect will happen.
    [property: JsonPropertyName("effect_chance")] int EffectChance,
    [property: JsonPropertyName("pp")] int Pp,

    // A value between -8 and 8. Sets the order in which moves are executed during battle. See Bulbapedia for greater detail.
    [property: JsonPropertyName("priority")] int Priority,
    [property: JsonPropertyName("power")] int Power,

    // A detail of normal and super contest combos that require this move.
    [property: JsonPropertyName("contest_combos")] ContestComboSets ContestCombos,

    // The type of appeal this move gives a Pokémon when used in a contest.
    [property: JsonPropertyName("contest_type")] NamedApiResource ContestType,

    // The effect the move has when used in a contest.
    [property: JsonPropertyName("contest_effect")] ApiResource ContestEffect,

    // The type of damage the move inflicts on the target, e.g. physical.
    [property: JsonPropertyName("damage_class")] NamedApiResource DamageClass,

    // The effect of this move listed in different languages.
    [property: JsonPropertyName("effect_entries")] List<VerboseEffect> EffectEntries,

    // The list of previous effects this move has had across version groups of the games.
    [property: JsonPropertyName("effect_changes")] List<AbilityEffectChange> EffectChanges,
    [property: JsonPropertyName("learned_by_pokemon")] List<NamedApiResource> LearnedByPokemon,

    // The flavour text of this move listed in different languages.
    [property: JsonPropertyName("flavor_text_entries")] List<MoveFlavourText> FlavourTextEntries,

    // The generation in which this move was introduced.
    [property: JsonPropertyName("generation")] NamedApiResource Generation,

    // A list of the machines that teach this move.
    [property: JsonPropertyName("machines")] List<MachineVersionDetail> Machines,

    // Metadata about this move
    [property: JsonPropertyName("meta")] MoveMetaData Meta,

    // The name of this resource listed in different languages.
    [property: JsonPropertyName("names")] List<Name> Names,

    // A list of move resource value changes across version groups of the game.
    [property: JsonPropertyName("past_values")] List<PastMoveStatValues> PastValues,

    // A list of stats this moves effects and how much it effects them.
    [property: JsonPropertyName("stat_changes")] List<MoveStatChange> StatChanges,

    // The effect the move has when used in a super contest.
    [property: JsonPropertyName("super_contest_effect")] ApiResource SuperContestEffect,

    // The type of target that will receive the effects of the attack.
    [property: JsonPropertyName("target")] NamedApiResource Target,

    // The elemental type of this move.
    [property: JsonPropertyName("type")] NamedApiResource Type
);
