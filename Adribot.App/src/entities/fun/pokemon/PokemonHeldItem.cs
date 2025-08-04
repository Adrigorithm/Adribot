using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record PokemonHeldItem(
    [property: JsonPropertyName("item")] NamedAPIResource Item,

    [property: JsonPropertyName("version_details")] List<PokemonHeldItemVersion> VersionDetails
);
