using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record Item(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("cost")] int Cost,
    [property: JsonPropertyName("fling_power")] int FlingPower,
    [property: JsonPropertyName("fling_effect")] NamedApiResource FlingEffect,
    [property: JsonPropertyName("attributes")] List<NamedApiResource> Attributes,
    [property: JsonPropertyName("category")] NamedApiResource Category,

    // The effect of this ability listed in different languages.
    [property: JsonPropertyName("effect_entries")] List<VerboseEffect> EffectEntries,

    // The flavour text of this ability listed in different languages.
    [property: JsonPropertyName("flavor_text_entries")] List<VersionGroupFlavourText> FlavourTextEntries,
    [property: JsonPropertyName("game_indices")] List<GenerationGameIndex> GameIndices,

    // The name of this item listed in different languages.
    [property: JsonPropertyName("names")] List<Name> Names,
    [property: JsonPropertyName("sprites")] ItemSprites ItemSprites,

    // A list of Pokémon that might be found in the wild holding this item.
    [property: JsonPropertyName("held_by_pokemon")] List<ItemHolderPokemon> HeldByPokemon,
    [property: JsonPropertyName("baby_trigger_for")] ApiResource BabyTriggerFor,

    // A list of the machines related to this item.
    [property: JsonPropertyName("machines")] List<MachineVersionDetail> Machines
);
