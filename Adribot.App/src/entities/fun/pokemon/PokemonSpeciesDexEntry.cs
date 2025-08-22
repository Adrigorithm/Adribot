using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record PokemonSpeciesDexEntry(
    // The index number within the Pokédex.
    [property: JsonPropertyName("entry_number")] int EntryNumber,

    // The Pokédex the referenced Pokémon species can be found in.
    [property: JsonPropertyName("pokedex")] NamedApiResource Pokedex
);
