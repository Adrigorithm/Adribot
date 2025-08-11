using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record ItemHolderPokemon(
    [property: JsonPropertyName("pokemon")] NamedApiResource Pokemon,

    // The details for the version that this item is held in by the Pokémon.
    [property: JsonPropertyName("version_details")] List<ItemHolderPokemonVersionDetail> VersionDetails
);
