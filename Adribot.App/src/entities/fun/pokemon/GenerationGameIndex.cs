using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class GenerationGameIndex
{
    [JsonPropertyName("game_index")]
    public int GameIndex { get; set; }

    [JsonPropertyName("generation")]
    public NamedApiResource Generation { get; set; }
}
