using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record MoveAilment(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    
    // A list of moves that cause this ailment.
    [property: JsonPropertyName("moves")] List<NamedApiResource> Moves,
    
    // The name of this resource listed in different languages.
    [property: JsonPropertyName("names")] List<Name> Names
);
