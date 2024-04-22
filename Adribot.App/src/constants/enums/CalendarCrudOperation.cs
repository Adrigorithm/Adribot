using DSharpPlus.SlashCommands;

namespace Adribot.src.constants.enums;

public enum CalendarCrudOperation
{
    [ChoiceName("new")]
    New,
    [ChoiceName("delete")]
    Delete,
    [ChoiceName("info")]
    Info,
    [ChoiceName("list")]
    List,
    [ChoiceName("next event")]
    Next
}
