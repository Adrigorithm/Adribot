using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class MachineVersionDetail
{
    // The machine that teaches a move from an item.
    [JsonPropertyName("machine")]
    public ApiResource Machine { get; set; }

    // The version group of this specific machine.
    [JsonPropertyName("version_group")]
    public NamedApiResource VersionGroup { get; set; }
}
