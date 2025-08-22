using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record MoveStatAffectSets(
    // A list of moves and how they change the referenced stat.
    [property: JsonPropertyName("increase")] List<MoveStatAffect> Increase,

    // A list of moves and how they change the referenced stat.
    [property: JsonPropertyName("decrease")] List<MoveStatAffect> Decrease
);
