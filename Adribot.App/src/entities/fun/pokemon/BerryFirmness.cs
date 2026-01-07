using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class BerryFirmness
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("berries")]
    public List<NamedApiResource> Berries { get; set; }

    // The name of this resource listed in different languages.
    [JsonPropertyName("names")]
    public List<Name> Names { get; set; }
}
