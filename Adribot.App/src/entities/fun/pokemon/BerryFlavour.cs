using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record BerryFlavour(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("berries")] List<FlavourBerryMap> Berries,

    // The contest type that correlates with this berry flavour.
    [property: JsonPropertyName("contest_type")] NamedApiResource ContestType,

    // The name of this resource listed in different languages.
    [property: JsonPropertyName("names")] List<Name> Names
);
