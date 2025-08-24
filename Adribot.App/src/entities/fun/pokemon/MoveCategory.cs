using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record MoveCategory(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,

    // A list of moves that fall into this category.
    [property: JsonPropertyName("moves")] List<NamedApiResource> Moves,

    // The description of this resource listed in different languages.
    [property: JsonPropertyName("descriptions")] List<Description> Descriptions
);
