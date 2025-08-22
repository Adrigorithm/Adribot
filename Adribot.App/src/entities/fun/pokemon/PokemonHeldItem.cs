using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record PokemonHeldItem(
    [property: JsonPropertyName("item")] NamedApiResource Item,
    [property: JsonPropertyName("version_details")] List<PokemonHeldItemVersion> VersionDetails
);
