using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.fun.pokemon;

public class PokemonSpecies
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    // The order in which species should be sorted. Based on National Dex order, except families are grouped together and sorted by stage.
    [JsonPropertyName("order")]
    public int Order { get; set; }

    // The chance of this Pokémon being female, in eighths; or -1 for genderless.
    [JsonPropertyName("gender_rate")]
    public int GenderRate { get; set; }

    // The base capture rate; up to 255. The higher the number, the easier the catch.
    [JsonPropertyName("capture_rate")]
    public int CaptureRate { get; set; }

    // The happiness when caught by a normal Pokéball; up to 255. The higher the number, the happier the Pokémon.
    [JsonPropertyName("base_happiness")]
    public int BaseHappiness { get; set; }

    [JsonPropertyName("is_baby")]
    public bool IsBaby { get; set; }

    [JsonPropertyName("is_legendary")]
    public bool IsLegendary { get; set; }

    [JsonPropertyName("is_mythical")]
    public bool IsMythical { get; set; }

    /*
     * Initial hatch counter: one must walk Y × (hatch_counter + 1) steps before this Pokémon's egg hatches, unless utilizing bonuses like Flame Body's.
     * Y varies per generation.
     * In Generations II, III, and VII, Egg cycles are 256 steps long.
     * In Generation IV, Egg cycles are 255 steps long.
     * In Pokémon Brilliant Diamond and Shining Pearl, Egg cycles are also 255 steps long, but are shorter on special dates.
     * In Generations V and VI, Egg cycles are 257 steps long.
     * In Pokémon Sword and Shield, and in Pokémon Scarlet and Violet, Egg cycles are 128 steps long.
     */
    [JsonPropertyName("hatch_counter")]
    public int HatchCounter { get; set; }

    // Whether this Pokémon has visual gender differences.
    [JsonPropertyName("has_gender_differences")]
    public bool HasGenderDifferences { get; set; }

    // Whether this Pokémon has multiple forms and can switch between them.
    [JsonPropertyName("forms_switchable")]
    public bool FormsSwitchable { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("growth_rate")]
    public NamedApiResource GrowthRate { get; set; }

    // A list of Pokedexes and the indexes reserved within them for this Pokémon species.
    [JsonPropertyName("pokedex_numbers")]
    public List<PokemonSpeciesDexEntry> PokedexNumbers { get; set; }

    // A list of egg groups this Pokémon species is a member of.
    [JsonPropertyName("egg_groups")]
    public List<NamedApiResource> EggGroups { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("color")]
    public NamedApiResource Colour { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("shape")]
    public NamedApiResource Shape { get; set; }

    // The Pokémon species that evolves into this Pokemon_species.
    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("evolves_from_species")]
    public NamedApiResource EvolvesFromSpecies { get; set; }

    // The evolution chain this Pokémon species is a member of.
    [JsonPropertyName("evolution_chain")]
    public ApiResource EvolutionChain { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("habitat")]
    public NamedApiResource Habitat { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("generation")]
    public NamedApiResource Generation { get; set; }

    // The name of this resource listed in different languages.
    [JsonPropertyName("names")]
    public List<Name> Names { get; set; }

    // A list of encounters that can be had with this Pokémon species in pal park.
    [JsonPropertyName("pal_park_encounters")]
    public List<PalParkEncounterArea> PalParkEncounters { get; set; }

    // A list of flavor text entries for this Pokémon species.
    [JsonPropertyName("flavor_text_entries")]
    public List<FlavourText> FlavourTextEntries { get; set; }

    // Descriptions of different forms Pokémon take on within the Pokémon species.
    [JsonPropertyName("form_descriptions")]
    public List<Description> FormDescriptions { get; set; }

    // The genus of this Pokémon species listed in multiple languages.
    [JsonPropertyName("genera")]
    public List<Genus> Genera { get; set; }

    // A list of the Pokémon that exist within this Pokémon species.
    [JsonPropertyName("varieties")]
    public List<PokemonSpeciesVariety> Varieties { get; set; }
}
