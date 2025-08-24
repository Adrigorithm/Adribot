using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record NatureStatAffectSets(
    // A list of natures and how they change the referenced stat.
    [property: JsonPropertyName("increase")] List<NamedApiResource> Increase,

    // A list of natures and how they change the referenced stat.
    [property: JsonPropertyName("decrease")] List<NamedApiResource> Decrease
);
