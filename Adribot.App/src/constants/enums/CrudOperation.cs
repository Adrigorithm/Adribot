using DSharpPlus.SlashCommands;

namespace Adribot.src.constants.enums;

public enum CrudOperation
{
    [ChoiceName("get")]
    Get,
    [ChoiceName("set")]
    Set,
    [ChoiceName("new")]
    New,
    [ChoiceName("delete")]
    Delete,
    [ChoiceName("info")]
    Info,
    [ChoiceName("list")]
    List
}
