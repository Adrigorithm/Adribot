using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class GrowthRateExperienceLevel
{
    // The level gained.
    [JsonPropertyName("level")]
    public int Level { get; set; }

    // The amount of experience required to reach the referenced level.
    [JsonPropertyName("experience")]
    public int Experience { get; set; }
}
