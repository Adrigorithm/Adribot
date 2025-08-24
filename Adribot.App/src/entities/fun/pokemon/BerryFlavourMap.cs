using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record BerryFlavourMap(
    // How powerful the referenced flavour is for this berry.
    [property: JsonPropertyName("potency")] int Potency,
    [property: JsonPropertyName("flavor")] NamedApiResource Flavour
);
