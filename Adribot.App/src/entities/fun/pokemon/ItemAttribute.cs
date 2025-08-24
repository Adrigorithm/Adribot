using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record ItemAttribute(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("items")] List<NamedApiResource> Items,

    // The name of this item attribute listed in different languages.
    [property: JsonPropertyName("names")] List<Name> Names,

    // The description of this item attribute listed in different languages.
    [property: JsonPropertyName("descriptions")] List<Description> Descriptions
);
