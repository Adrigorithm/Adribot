using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record PokemonSpecies(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,

    // The order in which species should be sorted. Based on National Dex order, except families are grouped together and sorted by stage.
    [property: JsonPropertyName("order")] int Order,

    // The chance of this Pokémon being female, in eighths; or -1 for genderless.
    [property: JsonPropertyName("gender_rate")] int GenderRate,

    // The base capture rate; up to 255. The higher the number, the easier the catch.
    [property: JsonPropertyName("capture_rate")] int CaptureRate,

    // The happiness when caught by a normal Pokéball; up to 255. The higher the number, the happier the Pokémon.
    [property: JsonPropertyName("base_happiness")] int BaseHappiness,
    [property: JsonPropertyName("is_baby")] bool IsBaby,
    [property: JsonPropertyName("is_legendary")] bool IsLegendary,
    [property: JsonPropertyName("is_mythical")] bool IsMythical,
    /*
     * Initial hatch counter: one must walk Y × (hatch_counter + 1) steps before this Pokémon's egg hatches, unless utilizing bonuses like Flame Body's.
     * Y varies per generation.
     * In Generations II, III, and VII, Egg cycles are 256 steps long.
     * In Generation IV, Egg cycles are 255 steps long.
     * In Pokémon Brilliant Diamond and Shining Pearl, Egg cycles are also 255 steps long, but are shorter on special dates.
     * In Generations V and VI, Egg cycles are 257 steps long.
     * In Pokémon Sword and Shield, and in Pokémon Scarlet and Violet, Egg cycles are 128 steps long.
     */
    [property: JsonPropertyName("hatch_counter")] int HatchCounter,

    // Whether this Pokémon has visual gender differences.
    [property: JsonPropertyName("has_gender_differences")] bool HasGenderDifferences,

    // Whether this Pokémon has multiple forms and can switch between them.
    [property: JsonPropertyName("forms_switchable")] bool FormsSwitchable,
    [property: JsonPropertyName("growth_rate")] NamedApiResource GrowthRate,

    // A list of Pokedexes and the indexes reserved within them for this Pokémon species.
    [property: JsonPropertyName("pokedex_numbers")] List<PokemonSpeciesDexEntry> PokedexNumbers,

    // A list of egg groups this Pokémon species is a member of.
    [property: JsonPropertyName("egg_groups")] List<NamedApiResource> EggGroups,
    [property: JsonPropertyName("color")] NamedApiResource Colour,
    [property: JsonPropertyName("shape")] NamedApiResource Shape,

    // The Pokémon species that evolves into this Pokemon_species.
    [property: JsonPropertyName("evolves_from_species")] NamedApiResource EvolvesFromSpecies,

    // The evolution chain this Pokémon species is a member of.
    [property: JsonPropertyName("evolution_chain")] ApiResource EvolutionChain,
    [property: JsonPropertyName("habitat")] NamedApiResource Habitat,
    [property: JsonPropertyName("generation")] NamedApiResource Generation,

    // The name of this resource listed in different languages.
    [property: JsonPropertyName("names")] List<Name> Names,

    // A list of encounters that can be had with this Pokémon species in pal park.
    [property: JsonPropertyName("pal_park_encounters")] List<PalParkEncounterArea> PalParkEncounters,

    // A list of flavor text entries for this Pokémon species.
    [property: JsonPropertyName("flavor_text_entries")] List<FlavourText> FlavourTextEntries,

    // Descriptions of different forms Pokémon take on within the Pokémon species.
    [property: JsonPropertyName("form_descriptions")] List<Description> FormDescriptions,

    // The genus of this Pokémon species listed in multiple languages.
    [property: JsonPropertyName("genera")] List<Genus> Genera,

    // A list of the Pokémon that exist within this Pokémon species.
    [property: JsonPropertyName("varieties")] List<PokemonSpeciesVariety> Varieties
);
