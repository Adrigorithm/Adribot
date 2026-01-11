using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.fun.pokemon;

public class PokemonForm
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    // The order in which forms should be sorted within all forms. Multiple forms may have equal order, in which case they should fall back on sorting by name.
    [JsonPropertyName("order")]
    public int Order { get; set; }

    // The order in which forms should be sorted within a species' forms.
    [JsonPropertyName("form_order")]
    public int FormOrder { get; set; }

    // True for exactly one form used as the default for each Pokémon.
    [JsonPropertyName("is_default")]
    public bool IsDefault { get; set; }

    // Whether or not this form can only happen during battle.
    [JsonPropertyName("is_battle_only")]
    public bool IsBattleOnly { get; set; }

    [JsonPropertyName("is_mega")]
    public bool IsMega { get; set; }

    [JsonPropertyName("form_name")]
    public string FormName { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("pokemon")]
    public NamedApiResource Pokemon { get; set; }

    // A list of details showing types this Pokémon form has.
    [JsonPropertyName("types")]
    public List<PokemonFormType> Types { get; set; }

    // A set of sprites used to depict this Pokémon form in the game.
    [JsonPropertyName("sprites")]
    public PokemonFormSprites Sprites { get; set; }

    // The version group this Pokémon form was introduced in.
    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("version_group")]
    public NamedApiResource VersionGroup { get; set; }

    // The form specific full name of this Pokémon form, or empty if the form does not have a specific name.
    [JsonPropertyName("names")]
    public List<Name> Names { get; set; }

    // The form specific form name of this Pokémon form, or empty if the form does not have a specific name.
    [JsonPropertyName("form_names")]
    public List<Name> FormNames { get; set; }
}
