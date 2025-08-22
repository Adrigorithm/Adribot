using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record Machine(
    [property: JsonPropertyName("id")] int Id,

    // The TM or HM item that corresponds to this machine.
    [property: JsonPropertyName("item")] NamedApiResource Item,

    // The move that is taught by this machine.
    [property: JsonPropertyName("move")] NamedApiResource Move,

    // The version group that this machine applies to.
    [property: JsonPropertyName("version_group")] NamedApiResource VersionGroup
);
