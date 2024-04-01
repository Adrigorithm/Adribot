using DSharpPlus.SlashCommands;

namespace Adribot.src.constants.enums;

public enum CrudOperation
{
    [ChoiceName("get")]
    GET,
    [ChoiceName("set")]
    SET,
    [ChoiceName("new")]
    NEW,
    [ChoiceName("delete")]
    DELETE,
    [ChoiceName("info")]
    INFO,
    [ChoiceName("list")]
    LIST
}
