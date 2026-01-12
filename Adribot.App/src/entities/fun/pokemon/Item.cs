using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.fun.pokemon;

public class Item
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("cost")]
    public int Cost { get; set; }

    [JsonPropertyName("fling_power")]
    public int FlingPower { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("fling_effect")]
    public NamedApiResource FlingEffect { get; set; }

    [JsonPropertyName("attributes")]
    public List<NamedApiResource> Attributes { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("category")]
    public NamedApiResource Category { get; set; }

    // The effect of this ability listed in different languages.
    [JsonPropertyName("effect_entries")]
    public List<VerboseEffect> EffectEntries { get; set; }

    // The flavour text of this ability listed in different languages.
    [JsonPropertyName("flavor_text_entries")]
    public List<VersionGroupFlavourText> FlavourTextEntries { get; set; }

    [JsonPropertyName("game_indices")]
    public List<GenerationGameIndex> GameIndices { get; set; }

    // The name of this item listed in different languages.
    [JsonPropertyName("names")]
    public List<Name> Names { get; set; }

    [JsonPropertyName("sprites")]
    public ItemSprites ItemSprites { get; set; }

    // A list of Pokémon that might be found in the wild holding this item.
    [JsonPropertyName("held_by_pokemon")]
    public List<ItemHolderPokemon> HeldByPokemon { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("baby_trigger_for")]
    public ApiResource BabyTriggerFor { get; set; }

    // A list of the machines related to this item.
    [JsonPropertyName("machines")]
    public List<MachineVersionDetail> Machines { get; set; }
}
