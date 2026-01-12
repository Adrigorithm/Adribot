using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.fun.pokemon;

public class MachineVersionDetail
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    // The machine that teaches a move from an item.
    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("machine")]
    public ApiResource Machine { get; set; }

    // The version group of this specific machine.
    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("version_group")]
    public NamedApiResource VersionGroup { get; set; }
}
