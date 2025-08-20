using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record EncounterCondition(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,

    // The name of this resource listed in different languages.
    [property: JsonPropertyName("names")] List<Name> Names,

    // A list of possible values for this encounter condition.
    [property: JsonPropertyName("values")] List<NamedApiResource> Values
);
