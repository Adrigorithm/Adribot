using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.fun.pokemon;

public class Move
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("accuracy")]
    public int Accuracy { get; set; }

    // The percent value of how likely this moves effect will happen.
    [JsonPropertyName("effect_chance")]
    public int EffectChance { get; set; }

    [JsonPropertyName("pp")]
    public int Pp { get; set; }

    // A value between -8 and 8. Sets the order in which moves are executed during battle. See Bulbapedia for greater detail.
    [JsonPropertyName("priority")]
    public int Priority { get; set; }

    [JsonPropertyName("power")]
    public int Power { get; set; }

    // A detail of normal and super contest combos that require this move.
    [JsonPropertyName("contest_combos")]
    public ContestComboSets ContestCombos { get; set; }

    // The type of appeal this move gives a Pokémon when used in a contest.
    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("contest_type")]
    public NamedApiResource ContestType { get; set; }

    // The effect the move has when used in a contest.
    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("contest_effect")]
    public ApiResource ContestEffect { get; set; }

    // The type of damage the move inflicts on the target, e.g. physical.
    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("damage_class")]
    public NamedApiResource DamageClass { get; set; }

    // The effect of this move listed in different languages.
    [JsonPropertyName("effect_entries")]
    public List<VerboseEffect> EffectEntries { get; set; }

    // The list of previous effects this move has had across version groups of the games.
    [JsonPropertyName("effect_changes")]
    public List<AbilityEffectChange> EffectChanges { get; set; }

    [JsonPropertyName("learned_by_pokemon")]
    public List<NamedApiResource> LearnedByPokemon { get; set; }

    // The flavour text of this move listed in different languages.
    [JsonPropertyName("flavor_text_entries")]
    public List<MoveFlavourText> FlavourTextEntries { get; set; }

    // The generation in which this move was introduced.
    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("generation")]
    public NamedApiResource Generation { get; set; }

    // A list of the machines that teach this move.
    [JsonPropertyName("machines")]
    public List<MachineVersionDetail> Machines { get; set; }

    // Metadata about this move
    [JsonPropertyName("meta")]
    public MoveMetaData Meta { get; set; }

    // The name of this resource listed in different languages.
    [JsonPropertyName("names")]
    public List<Name> Names { get; set; }

    // A list of move resource value changes across version groups of the game.
    [JsonPropertyName("past_values")]
    public List<PastMoveStatValues> PastValues { get; set; }

    // A list of stats this moves effects and how much it effects them.
    [JsonPropertyName("stat_changes")]
    public List<MoveStatChange> StatChanges { get; set; }

    // The effect the move has when used in a super contest.
    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("super_contest_effect")]
    public ApiResource SuperContestEffect { get; set; }

    // The type of target that will receive the effects of the attack.
    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("target")]
    public NamedApiResource Target { get; set; }

    // The elemental type of this move.
    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("type")]
    public NamedApiResource Type { get; set; }
}
