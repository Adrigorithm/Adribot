using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class BerryFlavourMap
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    // How powerful the referenced flavour is for this berry.
    [JsonPropertyName("potency")]
    public int Potency { get; set; }

    [JsonPropertyName("flavor")]
    public NamedApiResource Flavour { get; set; }
}
