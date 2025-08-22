using Discord.Interactions;

namespace Adribot.constants.enums;

public enum CalendarCrudOperation
{
    [ChoiceDisplay("new")]
    New,

    [ChoiceDisplay("delete")]
    Delete,

    [ChoiceDisplay("info")]
    Info,

    [ChoiceDisplay("list")]
    List,

    [ChoiceDisplay("next")]
    Next
}
