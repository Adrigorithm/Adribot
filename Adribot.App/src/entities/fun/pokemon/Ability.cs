using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class Ability
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    // Whether this ability originated in the main series of the video games.
    [JsonPropertyName("is_main_series")]
    public bool IsMainSeries { get; set; }

    [JsonPropertyName("generation")]
    public NamedApiResource Generation { get; set; }

    [JsonPropertyName("language")]
    public NamedApiResource Language { get; set; }

    [JsonPropertyName("names")]
    public List<Name> Names { get; set; }

    // The effect of this ability listed in different languages.
    [JsonPropertyName("effect_entries")]
    public List<VerboseEffect> EffectEntries { get; set; }

    // The list of previous effects this ability has had across version groups.
    [JsonPropertyName("effect_changes")]
    public List<AbilityEffectChange> EffectChanges { get; set; }

    // The flavour text of this ability listed in different languages.
    [JsonPropertyName("flavor_text_entries")]
    public List<AbilityFlavourText> FlavourTextEntries { get; set; }

    [JsonPropertyName("pokemon")]
    public List<AbilityPokemon> Pokemon { get; set; }
}
