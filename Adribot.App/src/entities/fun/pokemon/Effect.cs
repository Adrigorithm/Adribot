using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record Effect(
    [property: JsonPropertyName("effect")] string LocalisedEffect,
    [property: JsonPropertyName("language")] NamedApiResource Language
);
