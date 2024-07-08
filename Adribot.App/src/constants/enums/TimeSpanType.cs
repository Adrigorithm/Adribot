using Discord.Interactions;

namespace Adribot.src.constants.enums;

public enum TimeSpanType
{
    [ChoiceDisplay("seconds")]
    Seconds,

    [ChoiceDisplay("minutes")]
    Minutes,

    [ChoiceDisplay("hours")]
    Hours,

    [ChoiceDisplay("days")]
    Days,

    [ChoiceDisplay("weeks")]
    Weeks,

    [ChoiceDisplay("months")]
    Months,

    [ChoiceDisplay("years")]
    Years
}
