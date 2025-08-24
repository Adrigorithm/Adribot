using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record MachineVersionDetail(
    // The machine that teaches a move from an item.
    [property: JsonPropertyName("machine")] ApiResource Machine,

    // The version group of this specific machine.
    [property: JsonPropertyName("version_group")] NamedApiResource VersionGroup
);
