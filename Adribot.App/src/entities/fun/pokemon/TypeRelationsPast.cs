using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.fun.pokemon;

public class TypeRelationsPast
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    // The last generation in which the referenced type had the listed damage relations
    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("generation")]
    public NamedApiResource Generation { get; set; }

    // The damage relations the referenced type had up to and including the listed generation
    [JsonPropertyName("damage_relations")]
    public TypeRelations DamageRelations { get; set; }
}
