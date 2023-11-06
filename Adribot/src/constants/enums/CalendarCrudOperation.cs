using DSharpPlus.SlashCommands;

namespace Adribot.src.constants.enums
{
    public enum CalendarCrudOperation
    {
        [ChoiceName("new")]
        NEW,
        [ChoiceName("delete")]
        DELETE,
        [ChoiceName("info")]
        INFO,
        [ChoiceName("list")]
        LIST,
        [ChoiceName("next event")]
        NEXT
    }
}
