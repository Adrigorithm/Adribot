using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class TypeRelations
{
    [JsonPropertyName("no_damage_to")]
    public List<NamedApiResource> NoDamageTo { get; set; }

    [JsonPropertyName("half_damage_to")]
    public List<NamedApiResource> HalfDamageTo { get; set; }

    [JsonPropertyName("double_damage_to")]
    public List<NamedApiResource> DoubleDamageTo { get; set; }

    [JsonPropertyName("no_damage_from")]
    public List<NamedApiResource> NoDamageFrom { get; set; }

    [JsonPropertyName("half_damage_from")]
    public List<NamedApiResource> HalfDamageFrom { get; set; }

    [JsonPropertyName("double_damage_from")]
    public List<NamedApiResource> DoubleDamageFrom { get; set; }
}
