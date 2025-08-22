using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record Location(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("region")] NamedApiResource Region,
    [property: JsonPropertyName("names")] List<Name> Names,
    [property: JsonPropertyName("game_indices")] List<GenerationGameIndex> GameIndices,
    [property: JsonPropertyName("areas")] List<NamedApiResource> Areas
);
