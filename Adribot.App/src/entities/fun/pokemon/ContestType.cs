using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.fun.pokemon;

public class ContestType
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    // The berry flavour that correlates with this contest type.
    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("berry_flavor")]
    public NamedApiResource BerryFlavour { get; set; }

    // The name of this resource listed in different languages.
    [JsonPropertyName("names")]
    public List<Name> Names { get; set; }
}
