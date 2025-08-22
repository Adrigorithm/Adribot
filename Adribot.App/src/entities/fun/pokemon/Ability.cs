using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record Ability(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,

    // Whether this ability originated in the main series of the video games.
    [property: JsonPropertyName("is_main_series")] bool IsMainSeries,
    [property: JsonPropertyName("generation")] NamedApiResource Generation,
    [property: JsonPropertyName("language")] NamedApiResource Language,
    [property: JsonPropertyName("names")] List<Name> Names,

    // The effect of this ability listed in different languages.
    [property: JsonPropertyName("effect_entries")] List<VerboseEffect> EffectEntries,

    // The list of previous effects this ability has had across version groups.
    [property: JsonPropertyName("effect_changes")] List<AbilityEffectChange> EffectChanges,

    // The flavour text of this ability listed in different languages.
    [property: JsonPropertyName("flavor_text_entries")] List<AbilityFlavourText> FlavourTextEntries,
    [property: JsonPropertyName("pokemon")] List<AbilityPokemon> Pokemon
);
