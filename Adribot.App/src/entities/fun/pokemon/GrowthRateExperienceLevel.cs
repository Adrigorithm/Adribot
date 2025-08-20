using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record GrowthRateExperienceLevel(
    // The level gained.
    [property: JsonPropertyName("level")] int Level,

    // The amount of experience required to reach the referenced level.
    [property: JsonPropertyName("experience")] int Experience
);
