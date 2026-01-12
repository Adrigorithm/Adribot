using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.fun.pokemon;

public class MoveStatChange
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("change")]
    public int Change { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("stat")]
    public NamedApiResource Stat { get; set; }
}
