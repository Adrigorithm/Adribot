using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record ContestType(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,

    // The berry flavour that correlates with this contest type.
    [property: JsonPropertyName("berry_flavor")] NamedApiResource BerryFlavour,

    // The name of this resource listed in different languages.
    [property: JsonPropertyName("names")] List<Name> Names
);
