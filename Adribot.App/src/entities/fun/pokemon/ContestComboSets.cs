using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.fun.pokemon;

public class ContestComboSets
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    // A detail of moves this move can be used before or after, granting additional appeal points in contests.
    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("normal")]
    public ContestComboDetail Normal { get; set; }

    // A detail of moves this move can be used before or after, granting additional appeal points in super contests.
    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("super")]
    public ContestComboDetail Super { get; set; }
}
