using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class VersionGameIndex
{
    [JsonPropertyName("game_index")]
    public int GameIndex { get; set; }

    [JsonPropertyName("version")]
    public NamedApiResource Version { get; set; }
}
