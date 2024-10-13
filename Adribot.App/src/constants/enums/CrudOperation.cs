using Discord.Interactions;

namespace Adribot.constants.enums;

public enum CrudOperation
{
    [ChoiceDisplay("get")]
    Get,

    [ChoiceDisplay("set")]
    Set,

    [ChoiceDisplay("new")]
    New,

    [ChoiceDisplay("delete")]
    Delete,

    [ChoiceDisplay("info")]
    Info,

    [ChoiceDisplay("list")]
    List
}
