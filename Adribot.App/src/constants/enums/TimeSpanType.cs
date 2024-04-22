using DSharpPlus.SlashCommands;

namespace Adribot.src.constants.enums;

public enum TimeSpanType
{
    [ChoiceName("seconds")]
    Seconds,
    [ChoiceName("minutes")]
    Minutes,
    [ChoiceName("hours")]
    Hours,
    [ChoiceName("days")]
    Days,
    [ChoiceName("weeks")]
    Weeks,
    [ChoiceName("months")]
    Months,
    [ChoiceName("years")]
    Years
}
