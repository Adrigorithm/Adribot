using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record VerboseEffect(
    [property: JsonPropertyName("effect")] string Effect,
    [property: JsonPropertyName("short_effect")] string ShortEffect,
    [property: JsonPropertyName("language")] NamedApiResource Language
);
