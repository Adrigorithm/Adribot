using Discord.Interactions;

namespace Adribot.Constants.Enums;

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
