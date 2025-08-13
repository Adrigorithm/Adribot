using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record AwesomeName(
    [property: JsonPropertyName("awesome_name")] string LocalisedAwesomeName,
    [property: JsonPropertyName("language")] NamedApiResource Language
);
