using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record GrowthRate(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,

    // The formula used to calculate the rate at which the Pokémon species gains level.
    [property: JsonPropertyName("formula")] string Formula,

    // The descriptions of this characteristic listed in different languages.
    [property: JsonPropertyName("descriptions")] List<Description> Descriptions,

    // A list of levels and the amount of experienced needed to atain them based on this growth rate.
    [property: JsonPropertyName("levels")] List<GrowthRateExperienceLevel> Levels,

    // A list of Pokémon species that gain levels at this growth rate.
    [property: JsonPropertyName("pokemon_species")] List<NamedApiResource> PokemonSpecies
);
