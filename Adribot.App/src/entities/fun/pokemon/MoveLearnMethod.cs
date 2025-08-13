using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record MoveLearnMethod(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    
    // The description of this resource listed in different languages.
    [property: JsonPropertyName("descriptions")] List<Description> Descriptions,
    
    // The name of this resource listed in different languages.
    [property: JsonPropertyName("names")] List<Name> Names,
    
    // A list of version groups where moves can be learned through this method.
    [property: JsonPropertyName("version_groups")] List<NamedApiResource> VersionGroups
);
