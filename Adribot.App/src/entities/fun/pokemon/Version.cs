using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record Version(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("names")] List<Name> Names,
    [property: JsonPropertyName("version_group")] NamedApiResource VersionGroup
);
