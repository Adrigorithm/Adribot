using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record Effect(
    [property: JsonPropertyName("effect")] string LocalisedEffect,
    [property: JsonPropertyName("language")] NamedApiResource Language
);
