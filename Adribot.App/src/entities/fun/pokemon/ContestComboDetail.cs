using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record ContestComboDetail(
    // A list of moves to use before this move.
    [property: JsonPropertyName("use_before")] List<NamedApiResource> UseBefore,

    // A list of moves to use after this move.
    [property: JsonPropertyName("use_after")] List<NamedApiResource> UseAfter
);
