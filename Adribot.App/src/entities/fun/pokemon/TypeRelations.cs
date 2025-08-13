using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record TypeRelations(
    [property: JsonPropertyName("no_damage_to")] List<NamedApiResource> NoDamageTo,
    [property: JsonPropertyName("half_damage_to")] List<NamedApiResource> HalfDamageTo,
    [property: JsonPropertyName("double_damage_to")] List<NamedApiResource> DoubleDamageTo,
    [property: JsonPropertyName("no_damage_from")] List<NamedApiResource> NoDamageFrom,
    [property: JsonPropertyName("half_damage_from")] List<NamedApiResource> HalfDamageFrom,
    [property: JsonPropertyName("double_damage_from")] List<NamedApiResource> DoubleDamageFrom
);
