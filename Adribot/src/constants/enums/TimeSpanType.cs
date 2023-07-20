using DSharpPlus.SlashCommands;

namespace Adribot.constants.enums;

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
