using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record EncounterMethod(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("order")] int Order,

    // The name of this resource listed in different languages.
    [property: JsonPropertyName("names")] List<Name> Names
);
