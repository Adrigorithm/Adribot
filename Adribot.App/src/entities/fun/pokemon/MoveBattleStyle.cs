using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record MoveBattleStyle(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,

    // The name of this resource listed in different languages.
    [property: JsonPropertyName("names")] List<Name> Names
);
