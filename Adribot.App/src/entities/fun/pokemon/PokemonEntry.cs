using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record PokemonEntry(
    [property: JsonPropertyName("entry_number")] int EntryNumber,
    [property: JsonPropertyName("pokemon_species")] NamedApiResource PokemonSpecies
);
