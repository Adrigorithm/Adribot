using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record LocationArea(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,

    // The internal id of an API resource within game data.
    [property: JsonPropertyName("game_index")] int GameIndex,

    // A list of methods in which Pokémon may be encountered in this area and how likely the method will occur depending on the version of the game.
    [property: JsonPropertyName("encounter_method_rates")] List<EncounterMethodRate> EncounterMethodRates,
    [property: JsonPropertyName("location")] NamedApiResource Location,

    // The name of this resource listed in different languages.
    [property: JsonPropertyName("names")] List<Name> Names,

    // A list of Pokémon that can be encountered in this area along with version specific details about the encounter.
    [property: JsonPropertyName("pokemon_encounters")] List<PokemonEncounter> PokemonEncounters
);
