using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class Machine
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    // The TM or HM item that corresponds to this machine.
    [JsonPropertyName("item")]
    public NamedApiResource Item { get; set; }

    // The move that is taught by this machine.
    [JsonPropertyName("move")]
    public NamedApiResource Move { get; set; }

    // The version group that this machine applies to.
    [JsonPropertyName("version_group")]
    public NamedApiResource VersionGroup { get; set; }
}
