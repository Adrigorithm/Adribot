using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record EncounterConditionValue(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("condition")] NamedApiResource Condition,

    // The name of this resource listed in different languages.
    [property: JsonPropertyName("names")] List<Name> Names
);
