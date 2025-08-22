using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record PokemonForm(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,

    // The order in which forms should be sorted within all forms. Multiple forms may have equal order, in which case they should fall back on sorting by name.
    [property: JsonPropertyName("order")] int Order,

    // The order in which forms should be sorted within a species' forms.
    [property: JsonPropertyName("form_order")] int FormOrder,

    // True for exactly one form used as the default for each Pokémon.
    [property: JsonPropertyName("is_default")] bool IsDefault,

    // Whether or not this form can only happen during battle.
    [property: JsonPropertyName("is_battle_only")] bool IsBattleOnly,
    [property: JsonPropertyName("is_mega")] bool IsMega,
    [property: JsonPropertyName("form_name")] string FormName,
    [property: JsonPropertyName("pokemon")] NamedApiResource Pokemon,

    // A list of details showing types this Pokémon form has.
    [property: JsonPropertyName("types")] List<PokemonFormType> Types,

    // A set of sprites used to depict this Pokémon form in the game.
    [property: JsonPropertyName("sprites")] PokemonFormSprites Sprites,

    // The version group this Pokémon form was introduced in.
    [property: JsonPropertyName("version_group")] NamedApiResource VersionGroup,

    // The form specific full name of this Pokémon form, or empty if the form does not have a specific name.
    [property: JsonPropertyName("names")] List<Name> Names,

    // The form specific form name of this Pokémon form, or empty if the form does not have a specific name.
    [property: JsonPropertyName("form_names")] List<Name> FormNames
);
