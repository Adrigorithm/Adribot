using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.fun.pokemon;

public class LocationArea
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    // The internal id of an API resource within game data.
    [JsonPropertyName("game_index")]
    public int GameIndex { get; set; }

    // A list of methods in which Pokémon may be encountered in this area and how likely the method will occur depending on the version of the game.
    [JsonPropertyName("encounter_method_rates")]
    public List<EncounterMethodRate> EncounterMethodRates { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("location")]
    public NamedApiResource Location { get; set; }

    // The name of this resource listed in different languages.
    [JsonPropertyName("names")]
    public List<Name> Names { get; set; }

    // A list of Pokémon that can be encountered in this area along with version-specific details about the encounter.
    [JsonPropertyName("pokemon_encounters")]
    public List<PokemonEncounter> PokemonEncounters { get; set; }
}
