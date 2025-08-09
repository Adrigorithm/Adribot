using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record VersionGameIndex(
    [property: JsonPropertyName("game_index")] int GameIndex,
    [property: JsonPropertyName("version")] NamedApiResource Version
);
