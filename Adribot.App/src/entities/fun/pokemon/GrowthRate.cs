using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class GrowthRate
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    // The formula used to calculate the rate at which the Pokémon species gains level.
    [JsonPropertyName("formula")]
    public string Formula { get; set; }

    // The descriptions of this characteristic listed in different languages.
    [JsonPropertyName("descriptions")]
    public List<Description> Descriptions { get; set; }

    // A list of levels and the amount of experience needed to attain them based on this growth rate.
    [JsonPropertyName("levels")]
    public List<GrowthRateExperienceLevel> Levels { get; set; }

    // A list of Pokémon species that gain levels at this growth rate.
    [JsonPropertyName("pokemon_species")]
    public List<NamedApiResource> PokemonSpecies { get; set; }
}
