using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.fun.pokemon;

public class GenerationGameIndex
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("game_index")]
    public int GameIndex { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("generation")]
    public NamedApiResource Generation { get; set; }
}
