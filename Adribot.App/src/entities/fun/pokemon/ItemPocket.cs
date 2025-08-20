using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record ItemPocket(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,

    // A list of item categories that are relevant to this item pocket.
    [property: JsonPropertyName("categories")] List<NamedApiResource> Categories,

    // The name of this resource listed in different languages.
    [property: JsonPropertyName("names")] List<Name> Names
);
