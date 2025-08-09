using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record PokemonMove(
    [property: JsonPropertyName("move")] NamedApiResource Move,

    [property: JsonPropertyName("version_group_details")] List<PokemonMoveVersion> VersionGroupDetails
);
