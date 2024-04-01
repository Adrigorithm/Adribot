using DSharpPlus.SlashCommands;

namespace Adribot.src.constants.enums;

public enum TimeSpanType
{
    [ChoiceName("seconds")]
    SECONDS,
    [ChoiceName("minutes")]
    MINUTES,
    [ChoiceName("hours")]
    HOURS,
    [ChoiceName("days")]
    DAYS,
    [ChoiceName("weeks")]
    WEEKS,
    [ChoiceName("months")]
    MONTHS,
    [ChoiceName("years")]
    YEARS
}
