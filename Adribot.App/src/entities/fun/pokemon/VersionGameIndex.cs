using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.fun.pokemon;

public class VersionGameIndex
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("game_index")]
    public int GameIndex { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("version")]
    public NamedApiResource Version { get; set; }
}
