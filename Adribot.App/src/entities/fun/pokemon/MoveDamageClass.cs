using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record MoveDamageClass(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,

    // The description of this resource listed in different languages.
    [property: JsonPropertyName("descriptions")] List<Description> Descriptions,

    // A list of moves that fall into this damage class.
    [property: JsonPropertyName("moves")] List<NamedApiResource> Moves,

    // The name of this resource listed in different languages.
    [property: JsonPropertyName("names")] List<Name> Names
);
