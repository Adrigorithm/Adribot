using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.fun.pokemon;

public class Machine
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    // The TM or HM item that corresponds to this machine.
    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("item")]
    public NamedApiResource Item { get; set; }

    // The move that is taught by this machine.
    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("move")]
    public NamedApiResource Move { get; set; }

    // The version group that this machine applies to.
    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("version_group")]
    public NamedApiResource VersionGroup { get; set; }
}
