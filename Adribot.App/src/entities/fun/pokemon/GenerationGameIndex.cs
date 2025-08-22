using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record GenerationGameIndex(
    [property: JsonPropertyName("game_index")] int GameIndex,
    [property: JsonPropertyName("generation")] NamedApiResource Generation
);
