using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record ItemCategory(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("items")] List<NamedApiResource> Items,

    // The name of this item category listed in different languages.
    [property: JsonPropertyName("names")] List<Name> Names,

    // The pocket items in this category would be put in.
    [property: JsonPropertyName("pocket")] NamedApiResource Pocket
);
